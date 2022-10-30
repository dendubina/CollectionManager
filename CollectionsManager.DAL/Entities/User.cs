using System.Collections.Generic;
using CollectionsManager.DAL.Constants;
using CollectionsManager.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace CollectionsManager.DAL.Entities
{
    public class User : IdentityUser
    {
        public UserStatus Status { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
