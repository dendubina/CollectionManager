using System;
using CollectionsManager.DAL.Constants;
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
                Name = RoleNames.User.ToString(),
                NormalizedName = RoleNames.User.ToString().ToUpper(),
            },
            new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = RoleNames.Admin.ToString(),
                NormalizedName = RoleNames.Admin.ToString().ToUpper(),
            },
            new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = RoleNames.Tester.ToString(),
                NormalizedName = RoleNames.Tester.ToString().ToUpper(),
            },
        };
    }
}
