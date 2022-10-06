using System;
using System.Collections.Generic;
using Entities.DTO.CustomFieldValues;

namespace CollectionManager.WEB.Models.Items
{
    public class ItemInCollectionDetails
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IEnumerable<CustomFieldValueToReturnDto> CustomFieldValues { get; set; }
    }
}
