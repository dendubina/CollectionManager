using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.EF;
using Entities.EF.Models;

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

        public Task<Collection> GetCollectionAsync(Guid collectionId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateCollection(Collection collection)
        {
            throw new NotImplementedException();
        }

        public void DeleteCollection(Collection collection)
        {
            throw new NotImplementedException();
        }
    }
}
