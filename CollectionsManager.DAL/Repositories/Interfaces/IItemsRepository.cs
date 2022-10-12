using System;
using System.Linq;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.DAL.Repositories.Interfaces
{
    public interface IItemsRepository
    {
        IQueryable<Item> GetAll(bool trackChanges);

        IQueryable<Item> GetItem(Guid itemId, bool trackChanges);

        void CreateItem(Item item);

        void DeleteItem(Item item);
    }
}
