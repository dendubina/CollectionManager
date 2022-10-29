using System;
using System.Threading.Tasks;
using CollectionManager.WEB.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    public class LikesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> PutLike(Guid itemId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account");
            }

            await _unitOfWork.Likes.PutLikeAsync(itemId, User.GetUserId());

            return RedirectToItemDetails(itemId);
        }

        [Authorize]
        public async Task<IActionResult> RemoveLike(Guid itemId)
        {
            await _unitOfWork.Likes.RemoveLikeAsync(itemId, User.GetUserId());

            return RedirectToItemDetails(itemId);
        }

        private RedirectToActionResult RedirectToItemDetails(Guid itemId)
            => RedirectToAction("Details", "Items", new { itemId });
    }
}
