using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.EF;
using Entities.EF.Models;

namespace Repository
{
    public class ItemsRepository : RepositoryBase<Item>, IItemsRepository
    {
        public ItemsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemAsync(Guid itemId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Item item)
            => Create(item);

        public void DeleteItem(Item item)
            => Delete(item);
    }
}
