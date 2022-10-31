using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;
using CollectionsManager.BLL.Exceptions;
using CollectionsManager.BLL.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Constants;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
            => _userManager = userManager;

        public async Task<IEnumerable<UserToReturnDto>> GetAllAsync()
            => await _userManager.Users
                .Select(user => new UserToReturnDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Status = user.Status,
                    Roles = user.UserRoles.Select(x => x.Role.Name)
                }).ToArrayAsync();
        
        public async Task AddAdminRoleAsync(string userId, string currentUserId)
        {
            await CheckIsUserHasAccessAsync(currentUserId);

            var user = await _userManager.FindByIdAsync(userId);

            CheckIsUserExists(user, userId);

            await _userManager.AddToRoleAsync(user, RoleNames.Admin.ToString());
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, RoleNames.Admin.ToString()));
        }

        public async Task RemoveAdminRoleAsync(string userId, string currentUserId)
        {
            await CheckIsUserHasAccessAsync(currentUserId);

            var user = await _userManager.FindByIdAsync(userId);

            CheckIsUserExists(user, userId);

            await _userManager.RemoveFromRoleAsync(user, RoleNames.Admin.ToString());
            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, RoleNames.Admin.ToString()));
        }

        public async Task BlockAsync(string userId, string currentUserId)
            => await ChangeStatus(UserStatus.Blocked, userId, currentUserId);

        public async Task UnblockAsync(string userId, string currentUserId)
            => await ChangeStatus(UserStatus.Active, userId, currentUserId);

        private async Task ChangeStatus(UserStatus status, string userId, string currentUserId)
        {
            await CheckIsUserHasAccessAsync(currentUserId);

            var user = await _userManager.FindByIdAsync(userId);

            CheckIsUserExists(user, userId);

            user.Status = status;
            await _userManager.UpdateAsync(user);
        }

        private static void CheckIsUserExists(User user, string userId)
        {
            if (user is null)
            {
                throw new ResourceNotFoundException($"User with id {userId} not found");
            }
        }

        private async Task CheckIsUserHasAccessAsync(string userId)
        {
            if (!await _userManager.IsInAdminRoleAsync(userId))
            {
                throw new ForbiddenAccessException($"User with id {userId} doesn't have access");
            }
        }
    }
}
