using System.Linq;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        IQueryable<Comment> GetAll(bool trackChanges);

        void CreateComment(Comment comment);
    }
}
