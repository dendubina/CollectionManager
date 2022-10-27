using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserToReturnDto>> GetAllAsync();

        Task AddAdminRoleAsync(IEnumerable<string> userIds, string currentUserId);

        Task RemoveAdminRoleAsync(IEnumerable<string> userIds, string currentUserId);
    }
}
