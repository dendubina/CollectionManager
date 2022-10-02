using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.EF;
using Entities.EF.Models;

namespace Repository
{
    public class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
    {
        public CommentsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetCommentAsync(Guid commentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
