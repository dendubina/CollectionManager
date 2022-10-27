using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsManager.BLL.DTO.User;
using CollectionsManager.BLL.Exceptions;
using CollectionsManager.BLL.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Constants;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        
        private readonly IRepositoryManager _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,  IMapper mapper, IRepositoryManager unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserToReturnDto>> GetAllAsync()
            => await _userManager.Users
                .Select(user => new UserToReturnDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(x => x.Role.Name)
                })
                .ToArrayAsync();
        
        public async Task AddAdminRoleAsync(IEnumerable<string> userIds, string currentUserId)
        {
            if (!await _userManager.IsInAdminRoleAsync(currentUserId))
            {
                ThrowForbiddenAccessException(currentUserId);
            }

            var users = await _userManager.FindByIdsAsync(userIds).ToArrayAsync();

            foreach (var user in users)
            {
                await _userManager.AddToRoleAsync(user, RoleNames.Admin.ToString());
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, RoleNames.Admin.ToString()));
            }
        }

        public async Task RemoveAdminRoleAsync(IEnumerable<string> userIds, string currentUserId)
        {
            if (!await _userManager.IsInAdminRoleAsync(currentUserId))
            {
                ThrowForbiddenAccessException(currentUserId);
            }

            var users = await _userManager.FindByIdsAsync(userIds).ToArrayAsync();

            foreach (var user in users)
            {
                await _userManager.RemoveFromRoleAsync(user, RoleNames.Admin.ToString());
                await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, RoleNames.Admin.ToString()));
            }
        }

        private static void ThrowForbiddenAccessException(string currentUserId)
            => throw new ForbiddenAccessException($"User with id {currentUserId} doesn't have access");
    }
}
