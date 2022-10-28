using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.Items;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface IItemsService
    {
        Task<ItemDetailsToReturnDto> GetItemDetailsAsync(Guid itemId);

        Task<ItemToEditDto> GetItemToEditAsync(Guid itemId);

        Task<IEnumerable<LastAddedItemDto>> GetLastAddedItems(int count);

        Task CreateItemAsync(ItemToCreate item);

        Task UpdateItemAsync(ItemToEditDto item);

        Task DeleteItemAsync(Guid itemId, string currentUserId);
    }
}
