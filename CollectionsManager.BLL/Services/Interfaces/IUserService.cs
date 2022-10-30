using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserToReturnDto>> GetAllAsync();

        Task AddAdminRoleAsync(string userId, string currentUserId);

        Task RemoveAdminRoleAsync(string userId, string currentUserId);

        Task BlockAsync(string userId, string currentUserId);

        Task UnblockAsync(string userId, string currentUserId);
    }
}
