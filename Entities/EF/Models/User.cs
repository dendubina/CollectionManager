using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Entities.EF.Models
{
    public class User : IdentityUser
    {
        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
