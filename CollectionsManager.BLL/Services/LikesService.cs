using System;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;

namespace CollectionsManager.BLL.Services
{
    public class LikesService : ILikesService
    {
        private readonly IRepositoryManager _unitOfWork;

        public LikesService(IRepositoryManager unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task PutLikeAsync(Guid itemId, string authorId)
        {
            _unitOfWork.Likes.PutLike(new Like{ AuthorId = authorId, ItemId = itemId });
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveLikeAsync(Guid itemId, string authorId)
        {
            _unitOfWork.Likes.RemoveLike(authorId, itemId);
            await _unitOfWork.SaveAsync();
        }
    }
}
