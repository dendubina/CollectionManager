using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using CollectionManager.WEB.Models.Users;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize(Roles = nameof(RoleNames.Admin))]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            return View(_mapper.Map<IList<UserViewModel>>(users));
        }

        [HttpPost]
        public async Task<IActionResult> HandleChangingRoles(IList<UserViewModel> users, string submit)
        {
            var selectedUserId = users
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (selectedUserId is not null)
            {
                switch (submit)
                {
                    case "AddAdminRole":
                        await _unitOfWork.Users.AddAdminRoleAsync(selectedUserId, User.GetUserId());
                        break;

                    case "RemoveAdminRole":
                        await _unitOfWork.Users.RemoveAdminRoleAsync(selectedUserId, User.GetUserId());
                        break;

                    case "Block":
                        await _unitOfWork.Users.BlockAsync(selectedUserId, User.GetUserId());
                        break;

                    case "Unblock":
                        await _unitOfWork.Users.UnblockAsync(selectedUserId, User.GetUserId());
                        break;
                }
            }

            return RedirectToAction("Index");
        }
    }
}
