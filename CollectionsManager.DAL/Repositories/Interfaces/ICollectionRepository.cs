using System;
using System.Linq;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface ICollectionRepository
    {
        IQueryable<Collection> GetAll(bool trackChanges);

        IQueryable<Collection> GetCollection(Guid collectionId, bool trackChanges);

        void CreateCollection(Collection collection);

        void DeleteCollection(Collection collection);
    }
}
