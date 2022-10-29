using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using System.Linq;

namespace CollectionsManager.DAL.Repositories
{
    public class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
    {
        public CommentsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Comment> GetAll(bool trackChanges)
            => FindAll(trackChanges);

        public void CreateComment(Comment comment)
            => Create(comment);
    }
}
