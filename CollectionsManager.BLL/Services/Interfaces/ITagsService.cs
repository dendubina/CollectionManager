using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.Tags;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ITagsService
    {
        Task<IEnumerable<TagDto>> GetAllAsync();

        Task<IEnumerable<TagDto>> FindBySubstringAsync(string substring);

        Task<IEnumerable<MostPopularTagDto>> GetMostPopularTagsAsync(int count);
    }
}
