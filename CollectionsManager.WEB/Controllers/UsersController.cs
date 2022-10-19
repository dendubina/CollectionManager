using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionManager.WEB.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            await _unitOfWork.Users.AddAdminRole(new List<string> { User.GetUserId() });

            return Ok();
        }
    }
}
