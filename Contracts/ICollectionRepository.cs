﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();

        Task<Collection> GetCollectionDetails(Guid collectionId);
            
        IQueryable<Collection> GetCollectionAsync(Guid collectionId, bool trackChanges);

        Task<IEnumerable<Collection>> GetCollectionsByUser(Guid userId);

        void CreateCollection(Collection collection);

        void DeleteCollection(Collection collection);
    }
}
