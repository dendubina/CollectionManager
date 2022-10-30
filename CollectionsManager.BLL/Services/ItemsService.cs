using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.BLL.Exceptions;
using CollectionsManager.BLL.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IRepositoryManager _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISearchService _searchService;

        public ItemsService(IRepositoryManager unitOfWork, IMapper mapper, UserManager<User> userManager, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _searchService = searchService;
        }

        public async Task<ItemToEditDto> GetItemToEditAsync(Guid itemId)
        {
            var item = await _unitOfWork.Items
                .GetItem(itemId, trackChanges: false)
                .Include(x => x.Tags)
                .Include(x => x.CustomValues)
                .ThenInclude(x => x.Field)
                .Select(item => _mapper.Map<ItemToEditDto>(item))
                .FirstOrDefaultAsync();

            CheckIsExists(item, itemId);

            return item;
        }

        public async Task<ItemDetailsToReturnDto> GetItemDetailsAsync(Guid itemId)
        {
            var item = await _unitOfWork.Items
                .GetItem(itemId, trackChanges: false)
                .Include(x => x.Collection)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Likes)
                .Include(x => x.Tags)
                .Include(x => x.CustomValues)
                .ThenInclude(x => x.Field)
                .Select(item => _mapper.Map<ItemDetailsToReturnDto>(item))
                .FirstOrDefaultAsync();

            CheckIsExists(item, itemId);

            return item;
        }

        public async Task<IEnumerable<LastAddedItemDto>> GetLastAddedItemsAsync(int count)
        {
            return await _unitOfWork.Items.GetAll(trackChanges: false)
                .OrderByDescending(x => x.AddedDate)
                .Take(count)
                .Select(item => new LastAddedItemDto
                {
                    Name = item.Name,
                    AuthorName = item.Collection.Owner.UserName,
                    Id = item.Id,
                    CollectionId = item.CollectionId,
                }).ToArrayAsync();
        }

        public async Task<IEnumerable<FoundItemToReturnDto>> GetByTagAsync(string tag)
        {
            return await _unitOfWork.Items.GetAll(trackChanges: false)
                .Where(item => item.Tags.Select(t => t.Name).Contains(tag))
                .Select(item => new FoundItemToReturnDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    CollectionId = item.CollectionId,
                    CollectionName = item.Collection.Name,
                    OwnerName = item.Collection.Owner.UserName,
                    Tags = item.Tags.Select(x => x.Name),
                }).ToArrayAsync();
        }

        public async Task<ItemToCreate> GetItemToAddAsync(Guid collectionId)
            => await _unitOfWork.Collections
                .GetCollection(collectionId, trackChanges: false)
                .Include(x => x.CustomFields)
                .Select(collection => _mapper.Map<ItemToCreate>(collection))
                .FirstOrDefaultAsync();

        public async Task CreateItemAsync(ItemToCreate item)
        {
            var entity = _mapper.Map<Item>(item);
            entity.AddedDate = DateTime.Now;

            _unitOfWork.Items.CreateItem(entity);

            if (item.Tags is not null && item.Tags.Any())
            {
                var tags = await _unitOfWork.Tags.CreateTags(_mapper.Map<IEnumerable<Tag>>(item.Tags));
                entity.Tags = tags.ToArray();
            }

            await _unitOfWork.SaveAsync();
            await _searchService.AddItemsAsync(new[] { entity.Id });
        }

        public async Task UpdateItemAsync(ItemToEditDto item)
        {
            var entity = await _unitOfWork.Items
                .GetItem(item.Id, trackChanges: true)
                .Include(x => x.Collection)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync();

            CheckIsExists(entity, item.Id);
            await _userManager.CheckIsUserHasAccess(entity.Collection.OwnerId, item.CurrentUserId);

            _mapper.Map(item, entity);
            if (item.Tags is not null && item.Tags.Any())
            {
                var tags = await _unitOfWork.Tags.CreateTags(_mapper.Map<IEnumerable<Tag>>(item.Tags));
                entity.Tags = tags.ToArray();
            }

            await _unitOfWork.SaveAsync();
            await _searchService.UpdateItemsAsync(new[] { entity.Id });
        }

        public async Task DeleteItemAsync(Guid itemId, string currentUserId)
        {
            var entity = await _unitOfWork.Items
                .GetItem(itemId, trackChanges: false)
                .Include(x => x.Collection)
                .FirstOrDefaultAsync();

            CheckIsExists(entity, itemId);
            await _userManager.CheckIsUserHasAccess(entity.Collection.OwnerId, currentUserId);

            _unitOfWork.Items.DeleteItem(entity);
            await _searchService.DeleteItemsAsync(new[] { entity.Id });
            await _unitOfWork.SaveAsync();
        }

        private static void CheckIsExists(object entity, Guid entityId)
        {
            if (entity is null)
            {
                throw new ResourceNotFoundException($"Item with Id {entityId} not found in database");
            }
        }
    }
}
