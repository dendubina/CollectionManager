using System.Linq;
using System.Security.Claims;

namespace CollectionManager.WEB.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            var id = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return id is not null ? id.Value : string.Empty;
        }
           // => claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToString();
    }
}
