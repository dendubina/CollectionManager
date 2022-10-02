using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.EF.Models;

namespace Contracts
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();

        Task<Item> GetItemAsync(Guid itemId, bool trackChanges);

        void CreateItem(Item item);

        void DeleteItem(Item item);
    }
}
