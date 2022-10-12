using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserProfile> SignInAsync(SignInModel userData);

        Task<UserProfile> SignUpAsync(SignUpModel userData);

        Task Logout();
    }
}
