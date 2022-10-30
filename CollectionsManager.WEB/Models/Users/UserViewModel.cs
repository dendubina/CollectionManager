using System.Collections.Generic;
using CollectionsManager.DAL.Constants;

namespace CollectionManager.WEB.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public bool IsSelected { get; set; }
    }
}
