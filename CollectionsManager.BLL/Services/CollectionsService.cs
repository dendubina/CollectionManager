using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsManager.BLL.DTO.Collections;
using CollectionsManager.BLL.Exceptions;
using CollectionsManager.BLL.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.BLL.Services
{
    public class CollectionsService : ICollectionsService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _unitOfWork;
        private readonly IMapper _mapper;

        public CollectionsService(IRepositoryManager unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<LargestCollectionToReturnDto>> GetMostLargeCollectionsAsync(int count)
            => await _unitOfWork.Collections
                .GetAll(trackChanges: false)
                .Include(x => x.Owner)
                .Include(x => x.Items)
                .OrderByDescending(x => x.Items.Count)
                .Take(count)
                .Select(entity => _mapper.Map<LargestCollectionToReturnDto>(entity))
                .ToArrayAsync();

        public async Task<CollectionDetailsToReturnDto> GetCollectionDetailsAsync(Guid collectionId)
        {
            var collection = await _unitOfWork.Collections
                .GetCollection(collectionId, trackChanges: false)
                .Include(x => x.Owner)
                .Include(x => x.CustomFields)
                .Include(x => x.Items)
                .ThenInclude(x => x.CustomValues)
                .Select(entity => _mapper.Map<CollectionDetailsToReturnDto>(entity))
                .FirstOrDefaultAsync();

            CheckIsExists(collection, collectionId);

            return collection;
        }

        public async Task<IEnumerable<UsersCollectionToReturnDto>> GetCollectionsByUserAsync(Guid userId)
            => await _unitOfWork.Collections
                .GetAll(trackChanges: false)
                .Where(x => x.OwnerId == userId.ToString())
                .Include(x => x.Owner)
                .Include(x => x.Items)
                .Select(entity => _mapper.Map<UsersCollectionToReturnDto>(entity))
                .ToArrayAsync();

        public async Task<CollectionToManipulateDto> GetCollectionToEditAsync(Guid collectionId)
        {
            var collection = await _unitOfWork.Collections
                .GetCollection(collectionId, trackChanges: false)
                .Include(x => x.CustomFields)
                .Select(entity => _mapper.Map<CollectionToManipulateDto>(entity))
                .FirstOrDefaultAsync();

            CheckIsExists(collection, collectionId);

            return collection;
        }

        public async Task CreateCollection(CollectionToManipulateDto collection)
        {
            _unitOfWork.Collections.CreateCollection(_mapper.Map<Collection>(collection));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCollection(CollectionToManipulateDto collection)
        {
            var entity = await _unitOfWork.Collections
                .GetCollection(collection.Id, trackChanges: true)
                .Include(x => x.CustomFields)
                .FirstOrDefaultAsync();

            CheckIsExists(entity, collection.Id);
            await _userManager.CheckIsUserHasAccess(entity.OwnerId, collection.OwnerId);

            _mapper.Map(collection, entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCollection(Guid collectionId, string currentUserId)
        {
            var collection = await _unitOfWork.Collections
                .GetCollection(collectionId, trackChanges: false)
                .FirstOrDefaultAsync();

            CheckIsExists(collection, collectionId);
            await _userManager.CheckIsUserHasAccess(collection.OwnerId, currentUserId);

            _unitOfWork.Collections.DeleteCollection(collection);
            await _unitOfWork.SaveAsync();
        }

        private static void CheckIsExists(object entity, Guid entityId)
        {
            if (entity is null)
            {
                throw new ResourceNotFoundException($"Collection with Id {entityId} not found in database");
            }
        }
    }
}
