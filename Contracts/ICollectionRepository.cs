using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();

        Task<Collection> GetCollectionAsync(Guid collectionId, bool trackChanges);

        void CreateCollection(Collection collection);

        void DeleteCollection(Collection collection);
    }
}
