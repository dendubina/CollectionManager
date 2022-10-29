using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.SearchService.Models;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ISearchService
    {
        Task AddItemsAsync(IEnumerable<Guid> itemIds);

        Task UpdateItemsAsync(IEnumerable<Guid> itemIds);

        Task DeleteItemsAsync(IEnumerable<Guid> itemIds);

        Task<IEnumerable<SearchItem>> SearchBySubstringAsync(string substring);
    }
}
