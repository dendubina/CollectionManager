using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.EF;
using Entities.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
    {
        public CommentsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Comment>> GetCommentsByItemId(Guid itemId)
            => await FindByCondition(x => x.ItemId.Equals(itemId), trackChanges: false)
                .Include(x => x.Author)
                .ToArrayAsync();

        public void CreateComment(Comment comment)
            => Create(comment);
    }
}
