using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;
using CollectionsManager.BLL.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserToReturnDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task AddAdminRole(IEnumerable<string> userIds)
            => await _userManager
                .FindByIds(userIds)
                .ForEachAsync(x => _userManager.AddToRoleAsync(x, "admin"));

        public async Task RemoveAdminRole(IEnumerable<string> userIds)
            => await _userManager
                .FindByIds(userIds)
                .ForEachAsync(x => _userManager.RemoveFromRoleAsync(x, "admin"));
    }
}
