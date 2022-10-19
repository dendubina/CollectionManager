using System.Collections.Generic;
using CollectionsManager.BLL.DTO.User.Roles;

namespace CollectionsManager.BLL.DTO.User
{
    public class UserToReturnDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<RoleToReturnDto> Roles { get; set; }
    }
}
