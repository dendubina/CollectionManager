using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.Collections;

namespace CollectionsManager.BLL.Services.Interfaces
{
    public interface ICollectionsService
    {
        Task<IEnumerable<LargestCollectionToReturnDto>> GetMostLargeCollectionsAsync(int count);

        Task<CollectionDetailsToReturnDto> GetCollectionDetailsAsync(Guid collectionId);

        Task<IEnumerable<UsersCollectionToReturnDto>> GetCollectionsByUserAsync(Guid userId);

        Task<CollectionToManipulateDto> GetCollectionToEditAsync(Guid collectionId);

        Task CreateCollectionAsync(CollectionToManipulateDto collection);

        Task UpdateCollectionAsync(CollectionToManipulateDto collection);

        Task DeleteCollectionAsync(Guid collectionId, string currentUserId);
    }
}
