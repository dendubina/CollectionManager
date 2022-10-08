using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByItemId(Guid itemId);

        void CreateComment(Comment comment);
    }
}
