using System.Threading.Tasks;
using CollectionsManager.BLL.Exceptions;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CollectionsManager.BLL.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task CheckIsUserHasAccess(this UserManager<User> userManager, string entityOwnerId, string currentUserId)
        {
            var user = await userManager.FindByIdAsync(currentUserId);

            if (entityOwnerId.Equals(user.Id) || await userManager.IsInRoleAsync(user, "admin"))
            {
                return;
            }

            throw new ForbiddenAccessException($"User with id {currentUserId} has no access to operation");
        }
    }
}
