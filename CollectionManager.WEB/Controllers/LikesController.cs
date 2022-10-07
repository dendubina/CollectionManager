using System;
using System.Threading.Tasks;
using CollectionManager.WEB.Extensions;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class LikesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> PutLike(Guid itemId)
        {
            _unitOfWork.Likes.PutLike(itemId, Guid.Parse(User.GetUserId()));
            await _unitOfWork.SaveAsync();

            return RedirectToItemDetails(itemId);
        }

        public async Task<IActionResult> RemoveLike(Guid itemId)
        {
            _unitOfWork.Likes.RemoveLike(itemId, Guid.Parse(User.GetUserId()));
            await _unitOfWork.SaveAsync();

            return RedirectToItemDetails(itemId);
        }

        private RedirectToActionResult RedirectToItemDetails(Guid itemId)
            => RedirectToAction("Details", "Items", new { itemId });
    }
}
