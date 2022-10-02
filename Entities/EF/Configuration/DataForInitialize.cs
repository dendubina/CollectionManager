using Microsoft.AspNetCore.Identity;

namespace Entities.EF.Configuration
{
    internal static class DataForInitialize
    {
        internal static readonly IdentityRole[] Roles =
        {
            new IdentityRole
            {
                Name = "user",
                NormalizedName = "USER",
            },
            new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
            },
        };
    }
}
