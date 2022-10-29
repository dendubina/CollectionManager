using System;
using System.Threading.Tasks;
using CollectionManager.WEB.Extensions;
using CollectionsManager.BLL.DTO.Comments;
using CollectionsManager.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment(CommentToCreateDto comment)
        {
            comment.AuthorId = User.GetUserId();

            await _unitOfWork.Comments.CreateComment(comment);

            return RedirectToAction("Details", "Items", new { comment.ItemId });
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsForItem(Guid itemId)
            => Json(await _unitOfWork.Comments.GetCommentsByItemId(itemId));
    }
}
