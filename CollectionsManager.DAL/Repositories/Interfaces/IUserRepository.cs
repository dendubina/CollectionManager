using System.Linq;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll(bool trackChanges);
    }
}
