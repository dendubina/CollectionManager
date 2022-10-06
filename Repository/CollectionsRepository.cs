﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.EF;
using Entities.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CollectionsRepository : RepositoryBase<Collection>, ICollectionRepository
    {
        public CollectionsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Collection> GetCollectionDetails(Guid collectionId)
            => await FindByCondition(x => x.Id.Equals(collectionId), trackChanges: false)
                .Include(x => x.CustomFields)
                .Include(x => x.Items)
                .ThenInclude(x => x.CustomValues)
                .ThenInclude(x => x.Field)
                .FirstOrDefaultAsync();

        public IQueryable<Collection> GetCollectionAsync(Guid collectionId, bool trackChanges)
            => FindByCondition(x => x.Id.Equals(collectionId), trackChanges);

        public async Task<IEnumerable<Collection>> GetCollectionsByUser(Guid userId)
            => await FindAll()
                .Include(x => x.Items)
                .Include(x => x.Owner)
                .Where(x => x.OwnerId == userId.ToString())
                .ToArrayAsync();

        public void CreateCollection(Collection collection)
            => Create(collection);

        public void DeleteCollection(Collection collection)
            => Delete(collection);
    }
}
