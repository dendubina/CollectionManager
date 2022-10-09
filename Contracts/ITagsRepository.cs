using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ITagsRepository
    {
        IQueryable<Tag> GetAll();

        Task<IEnumerable<Tag>> FindBySubstring(string substring);

        Task<IEnumerable<Tag>> CreateTags(IEnumerable<Tag> tags);
    }
}
