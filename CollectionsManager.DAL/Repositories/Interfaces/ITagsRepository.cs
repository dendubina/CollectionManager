using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface ITagsRepository
    {
        IQueryable<Tag> GetAll(bool trackChanges);

        Task<IEnumerable<Tag>> CreateTags(IEnumerable<Tag> tags);
    }
}
