using System;
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

        public ItemsService(IRepositoryManager unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ItemToEditDto> GetItemToEditAsync(Guid itemId)
        {
            var entity = await _unitOfWork.Items
                .GetItem(itemId, trackChanges: false)
                .Select(item => _mapper.Map<ItemToEditDto>(item))
                .FirstOrDefaultAsync();

            CheckIsExists(entity, itemId);

            return entity;
        }

        public async Task<ItemDetailsToReturnDto> GetItemDetailsAsync(Guid itemId)
        {
            var entity = await _unitOfWork.Items
                .GetItem(itemId, trackChanges: false)
                .Select(item => _mapper.Map<ItemDetailsToReturnDto>(item))
                .FirstOrDefaultAsync();

            CheckIsExists(entity, itemId);

            return entity;
        }

        public async Task CreateItemAsync(ItemToCreate item)
        {
            var entity = _mapper.Map<Item>(item);
            _unitOfWork.Items.CreateItem(entity);

            if (item.Tags is not null && item.Tags.Any())
            {
                var tags = await _unitOfWork.Tags.CreateTags(item.Tags);
                entity.Tags = tags.ToList();
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateItemAsync(ItemToEditDto item)
        {
            var entity = await _unitOfWork.Items
                .GetItem(item.Id, trackChanges: true)
                .Include(x => x.Collection)
                .FirstOrDefaultAsync();

            CheckIsExists(entity, item.Id);
            await _userManager.CheckIsUserHasAccess(entity.Collection.OwnerId, item.CurrentUserId);

            _mapper.Map(item, entity);
            await _unitOfWork.SaveAsync();
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
