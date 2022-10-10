using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();

        Task<IEnumerable<Collection>> GetMostLargeCollections(int count);

        Task<Collection> GetCollectionDetailsAsync(Guid collectionId);
            
        IQueryable<Collection> GetCollection(Guid collectionId, bool trackChanges);

        Task<IEnumerable<Collection>> GetCollectionsByUserAsync(Guid userId);

        void CreateCollection(Collection collection);

        void DeleteCollection(Collection collection);
    }
}
