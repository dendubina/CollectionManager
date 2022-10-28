using System.Linq;
using System.Security.Claims;
using CollectionsManager.DAL.Constants;

namespace CollectionManager.WEB.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.First(x => x.Type.Equals(ClaimTypes.Name)).Value;

        public static bool IsUserHasAccess(this ClaimsPrincipal claimsPrincipal, string ownerId)
            => claimsPrincipal.Identity.IsAuthenticated && GetUserId(claimsPrincipal) == ownerId || claimsPrincipal.IsInRole(RoleNames.Admin.ToString());
    }
}
