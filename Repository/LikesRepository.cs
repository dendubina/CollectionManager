using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Contracts;
using Entities.EF;
using Entities.EF.Models;

namespace Repository
{
    public class LikesRepository : RepositoryBase<Like>, ILikesRepository
    {
        public LikesRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void PutLike(Guid itemId, Guid authorId)
            => Create(new Like { ItemId = itemId, AuthorId = authorId.ToString() });


        public void RemoveLike(Guid itemId, Guid authorId)
            => Delete(DbContext.Likes.First(x => x.ItemId.Equals(itemId) && x.AuthorId == authorId.ToString()));
    }
}
