using System;
using System.Collections.Generic;
using CollectionsManager.BLL.DTO.CustomFieldValues;

namespace CollectionsManager.BLL.DTO.Items
{
    public class ItemInCollectionDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IEnumerable<CustomFieldValueToReturnDto> CustomFieldValues { get; set; }
    }
}
