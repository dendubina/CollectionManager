using System.Threading.Tasks;
using Entities.DTO.User;

namespace Contracts
{
    public interface IAuthService
    {
        Task<UserProfile> SignInAsync(SignInModel userData);

        Task<UserProfile> SignUpAsync(SignUpModel userData);

        Task Logout();
    }
}
