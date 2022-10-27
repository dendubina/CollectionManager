using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CollectionsManager.DAL.Entities.Users
{
    public class Role : IdentityRole<string>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
