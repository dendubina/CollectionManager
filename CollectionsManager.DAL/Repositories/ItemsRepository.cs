using System;
using System.Linq;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Entities;
using CollectionsManager.DAL.Repositories.Interfaces;

namespace CollectionsManager.DAL.Repositories
{
    public class ItemsRepository : RepositoryBase<Item>, IItemsRepository
    {
        public ItemsRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Item> GetAll(bool trackChanges)
            => FindAll(trackChanges);

        public IQueryable<Item> GetItem(Guid itemId, bool trackChanges)
            => FindByCondition(x => x.Id.Equals(itemId), trackChanges);

        public void CreateItem(Item item)
            => Create(item);

        public void DeleteItem(Item item)
        {
            DbContext.CustomFieldValues.RemoveRange(DbContext.CustomFieldValues.Where(x => x.ItemId.Equals(item.Id)));
            Delete(item);
        }


        /* public Task<IEnumerable<Item>> GetAllItemsAsync()
         {
             throw new NotImplementedException();
         }

         public IQueryable<Item> GetItem(Guid itemId, bool trackChanges)
             => FindByCondition(x => x.Id.Equals(itemId), trackChanges);         

         public void CreateItem(Item item)
             => Create(item);

         public void DeleteItem(Item item)
         {
             DbContext.CustomFieldValues.RemoveRange(DbContext.CustomFieldValues.Where(x => x.ItemId.Equals(item.Id)));
             Delete(item);
         }*/


    }
}
