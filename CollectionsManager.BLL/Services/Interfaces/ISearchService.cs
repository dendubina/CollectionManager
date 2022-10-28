using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.SearchService.Models;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ISearchService
    {
        Task AddItemAsync(Guid itemId);

        Task<IEnumerable<SearchItem>> SearchBySubstringAsync(string substring);
    }
}
