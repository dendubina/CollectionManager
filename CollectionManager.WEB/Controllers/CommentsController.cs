using System.Threading.Tasks;
using AutoMapper;
using CollectionManager.WEB.Extensions;
using Contracts;
using Entities.DTO.Comments;
using Entities.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(CommentToCreateDto comment)
        {
            var entity = _mapper.Map<Comment>(comment);
            entity.AuthorId = User.GetUserId();

            _unitOfWork.Comments.CreateComment(entity);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Details", "Items", new { comment.ItemId });
        }
    }
}
