using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsManager.BLL.DTO.Comments;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepositoryManager _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;

        public CommentsService(IRepositoryManager unitOfWork, IMapper mapper, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _searchService = searchService;
        }

        public async Task<IEnumerable<CommentToReturnDto>> GetCommentsByItemId(Guid itemId)
            => await _unitOfWork.Comments
                .GetAll(trackChanges: false)
                .Where(x => x.ItemId.Equals(itemId))
                .Include(x => x.Author)
                .Select(comment => _mapper.Map<CommentToReturnDto>(comment))
                .ToArrayAsync();

        public async Task CreateComment(CommentToCreateDto comment)
        {
            _unitOfWork.Comments.CreateComment(_mapper.Map<Comment>(comment));

            await _unitOfWork.SaveAsync();
            await _searchService.UpdateItemsAsync(new[] { comment.ItemId });
        }
    }
}
