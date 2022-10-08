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
    public class ItemsRepository : RepositoryBase<Item>, IItemsRepository
    {
        public ItemsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Item> GetItem(Guid itemId, bool trackChanges)
            => FindByCondition(x => x.Id.Equals(itemId), trackChanges);

        public async Task<Item> GetItemDetailsAsync(Guid itemId)
            => await FindByCondition(x => x.Id.Equals(itemId), trackChanges: false)
                .Include(x => x.Collection)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Likes)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Include(x => x.CustomValues)
                .ThenInclude(x => x.Field)
                .FirstOrDefaultAsync();

        public void CreateItem(Item item)
            => Create(item);

        public void DeleteItem(Item item)
        {
            DbContext.CustomFieldValues.RemoveRange(DbContext.CustomFieldValues.Where(x => x.ItemId.Equals(item.Id)));
            Delete(item);
        }

    }
}
