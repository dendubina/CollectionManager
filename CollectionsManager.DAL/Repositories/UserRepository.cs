using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using System.Linq;
using CollectionsManager.DAL.EF;

namespace CollectionsManager.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
            
        }

        public IQueryable<User> GetAll(bool trackChanges)
            => FindAll(trackChanges);
    }
}
