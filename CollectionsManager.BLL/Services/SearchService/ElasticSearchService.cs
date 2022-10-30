using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.BLL.Services.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsManager.BLL.Services.SearchService.Models.Options;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nest;

namespace CollectionsManager.BLL.Services.SearchService
{
    public class ElasticSearchService : ISearchService
    {
        private readonly string _itemsIndex;
        private readonly IElasticClient _client;
        private readonly IRepositoryManager _unitOfWork;

        public ElasticSearchService(IElasticClient client, IRepositoryManager unitOfWork, IOptions<ElasticSearchOptions> options)
        {
            _itemsIndex = options.Value.ItemsIndex;
            _client = client;
            _unitOfWork = unitOfWork;
        }

        public async Task AddItemsAsync(IEnumerable<Guid> itemIds)
        {
            var items = await GetFromDatabase(itemIds);

            foreach (var item in items)
            {
                await _client.IndexAsync(item, request => request.Index(_itemsIndex));
            }
        }

        public async Task UpdateItemsAsync(IEnumerable<Guid> itemIds)
        {
            var items = await GetFromDatabase(itemIds);

            foreach (var item in items)
            {
                await _client.UpdateAsync<SearchItem, SearchItem>(item.Id, x => x.Index(_itemsIndex).Doc(item));
            }
        }

        public async Task DeleteItemsAsync(IEnumerable<Guid> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                await _client.DeleteAsync<SearchItem>(itemId, i => i.Index(_itemsIndex));
            }
        }

        public async Task<IEnumerable<SearchItem>> SearchBySubstringAsync(string substring)
        {
            var response = await _client.SearchAsync<SearchItem>(s => s
                .Index(_itemsIndex)
                .Query(q => q
                    .QueryString(sq => sq.Query($"*{substring}*").AllowLeadingWildcard())));

            return response.Documents;
        }

        private async Task<IEnumerable<SearchItem>> GetFromDatabase(IEnumerable<Guid> itemIds)
        {
            return await _unitOfWork.Items
                .GetAll(trackChanges: false)
                .Where(x => itemIds.Any(id => id.Equals(x.Id)))
                .Select(item => new SearchItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    CollectionId = item.CollectionId,
                    CollectionName = item.Collection.Name,
                    OwnerName = item.Collection.Owner.UserName,
                    CustomFields = item.CustomValues.Select(value => new CustomField
                    {
                        Name = value.Field.Name,
                        Value = value.Value,
                    }),
                    Tags = item.Tags.Select(x => x.Name),
                    Comments = item.Comments.Select(comment => new Comment
                    {
                        Author = comment.Author.UserName,
                        Text = comment.Text
                    })
                }).ToArrayAsync();
        }
    }
}
