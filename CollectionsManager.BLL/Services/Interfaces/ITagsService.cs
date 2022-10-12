using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ITagsService
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<IEnumerable<Tag>> FindBySubstringAsync(string substring);
    }
}
