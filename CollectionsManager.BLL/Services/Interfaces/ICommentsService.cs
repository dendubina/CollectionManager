using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.Comments;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<IEnumerable<CommentToReturnDto>> GetCommentsByItemId(Guid itemId);

        Task CreateComment(CommentToCreateDto comment);
    }
}
