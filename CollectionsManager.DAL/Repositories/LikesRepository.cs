using System;
using System.Linq;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;

namespace CollectionsManager.DAL.Repositories
{
    public class LikesRepository : RepositoryBase<Like>, ILikesRepository
    {
        public LikesRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void PutLike(Like like)
            => Create(like);

        public void RemoveLike(string authorId, Guid itemId)
            => Delete(DbContext.Likes.First(x => x.ItemId.Equals(itemId) && x.AuthorId == authorId));


        /* public void PutLike(Guid itemId, Guid authorId)
             => Create(new Like { ItemId = itemId, AuthorId = authorId.ToString() });


         public void RemoveLike(Guid itemId, Guid authorId)
             => Delete(DbContext.Likes.First(x => x.ItemId.Equals(itemId) && x.AuthorId == authorId.ToString()));*/

    }
}
