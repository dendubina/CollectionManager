using System;
using System.Collections.Generic;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.DAL.Constants;

namespace CollectionsManager.BLL.DTO.Collections
{
    public class CollectionDetailsToReturnDto
    {
        public Guid Id { get; set; }

        public string Name  { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public CollectionCategory Category { get; set; }

        public IEnumerable<ItemInCollectionDetailsDto> ItemInCollectionDetails { get; set; }   
    }
}
