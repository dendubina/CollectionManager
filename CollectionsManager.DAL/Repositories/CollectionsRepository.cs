using System;
using System.Linq;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;

namespace CollectionsManager.DAL.Repositories
{
    public class CollectionsRepository : RepositoryBase<Collection>, ICollectionRepository
    {
        public CollectionsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Collection> GetAll(bool trackChanges)
            => FindAll(trackChanges);

        public IQueryable<Collection> GetCollection(Guid collectionId, bool trackChanges)
            => FindByCondition(x => x.Id.Equals(collectionId), trackChanges);

        public void CreateCollection(Collection collection)
            => Create(collection);

        public void DeleteCollection(Collection collection)
            => Delete(collection);
    }
}
