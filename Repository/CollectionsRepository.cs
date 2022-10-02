using System;
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

        public Task<Collection> GetCollectionAsync(Guid collectionId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Collection>> GetCollectionsByUser(Guid userId)
            => await FindAll()
                .Include(x => x.Items)
                .Where(x => x.OwnerId == userId.ToString())
                .ToArrayAsync();

        public void CreateCollection(Collection collection)
            => Create(collection);

        public void DeleteCollection(Collection collection)
        {
            throw new NotImplementedException();
        }
    }
}
