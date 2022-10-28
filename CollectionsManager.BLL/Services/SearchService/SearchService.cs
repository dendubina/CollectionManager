using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.BLL.Services.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace CollectionsManager.BLL.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly ElasticClient _client;
        private readonly IRepositoryManager _unitOfWork;

        public SearchService(ElasticClient client, IRepositoryManager unitOfWork)
        {
            _client = client;
            _unitOfWork = unitOfWork;
        }

        public async Task AddItemAsync(Guid itemId)
        {
            var item = await GetFromDb(itemId);
            
            await _client.IndexAsync(item, request => request.Index("items"));
          //  return Task.CompletedTask;
        }

        public Task<IEnumerable<SearchItem>> SearchBySubstringAsync(string substring)
        {
            throw new NotImplementedException();
        }

        private async Task<SearchItem> GetFromDb(Guid id)
            => await _unitOfWork.Items
                .GetItem(id, trackChanges: false)
                .Select(item => new SearchItem
                {
                    Id = item.Id,
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
                }).FirstOrDefaultAsync();
    }
}
