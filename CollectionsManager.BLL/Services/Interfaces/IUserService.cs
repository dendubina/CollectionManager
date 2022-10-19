using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserToReturnDto>> GetAll();

        Task AddAdminRole(IEnumerable<string> userIds);

        Task RemoveAdminRole(IEnumerable<string> userIds);
    }
}
