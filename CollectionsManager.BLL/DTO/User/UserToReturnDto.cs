using System.Collections.Generic;
using CollectionsManager.DAL.Constants;

namespace CollectionsManager.BLL.DTO.User
{
    public class UserToReturnDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
