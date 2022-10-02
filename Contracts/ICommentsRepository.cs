using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();

        Task<Comment> GetCommentAsync(Guid commentId, bool trackChanges);

        void CreateComment(Comment comment);

        void DeleteComment(Comment comment);
    }
}
