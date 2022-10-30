using System.Threading.Tasks;
using CollectionsManager.BLL.Exceptions;
using CollectionsManager.DAL.Constants;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CollectionsManager.BLL.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task CheckIsUserHasAccess(this UserManager<User> userManager, string entityOwnerId, string currentUserId)
        {
            var user = await userManager.FindByIdAsync(currentUserId);

            if (!user.Status.Equals(UserStatus.Blocked) && entityOwnerId.Equals(user.Id) || await userManager.IsInRoleAsync(user, RoleNames.Admin.ToString()))
            {
                return;
            }

            throw new ForbiddenAccessException($"User with id {currentUserId} has no access to operation");
        }

        public static async Task<bool> IsInAdminRoleAsync(this UserManager<User> userManager, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return await userManager.IsInRoleAsync(user, RoleNames.Admin.ToString()) && !user.Status.Equals(UserStatus.Blocked);
        }
          
    }
}
