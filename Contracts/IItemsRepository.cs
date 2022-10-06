using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();

        IQueryable<Item> GetItem(Guid itemId, bool trackChanges);

        Task<Item> GetItemDetailsAsync(Guid itemId);

        void CreateItem(Item item);

        void DeleteItem(Item item);
    }
}
