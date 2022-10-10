using System.Linq;
using System.Security.Claims;

namespace CollectionManager.WEB.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claims)
            => claims.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value.ToString();

        public static bool IsUserHasAccess(this ClaimsPrincipal claims, string ownerId)
            => GetUserId(claims) == ownerId || claims.IsInRole("admin");
    }
}
