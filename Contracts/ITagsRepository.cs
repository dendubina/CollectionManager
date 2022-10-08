using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ITagsRepository
    {
        Task<IEnumerable<Tag>> GetAll();

        Task<IEnumerable<Tag>> FindBySubstring(string substring);

        Task CreateTags(IEnumerable<Tag> tags);
    }
}
