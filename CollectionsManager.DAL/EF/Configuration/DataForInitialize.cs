using System;
using CollectionsManager.DAL.Entities.Users;

namespace CollectionsManager.DAL.EF.Configuration
{
    internal static class DataForInitialize
    {
        internal static readonly Role[] Roles =
        {
            new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "user",
                NormalizedName = "USER",
            },
            new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "ADMIN",
            },
        };
    }
}
