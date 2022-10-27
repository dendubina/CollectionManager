using System.Collections.Generic;
using System.Linq;
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

            if (entityOwnerId.Equals(user.Id) || await userManager.IsInRoleAsync(user, RoleNames.Admin.ToString()))
            {
                return;
            }

            throw new ForbiddenAccessException($"User with id {currentUserId} has no access to operation");
        }

        public static IQueryable<User> FindByIdsAsync(this UserManager<User> userManager, IEnumerable<string> userIds)
            => userManager.Users.Where(x => userIds.Any(f => f == x.Id));

        public static async Task<bool> IsInAdminRoleAsync(this UserManager<User> userManager, string userId)
            => await userManager.IsInRoleAsync(await userManager.FindByIdAsync(userId), RoleNames.Admin.ToString());
    }
}
