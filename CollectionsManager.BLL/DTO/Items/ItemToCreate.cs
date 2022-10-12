using System;
using System.Collections.Generic;
using CollectionsManager.BLL.DTO.CustomFields;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.DTO.Items
{
    public class ItemToCreate
    {
        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IList<CustomFieldValueToManipulateDto> CustomFieldValuesToCreate { get; set; }

        public IList<Tag> Tags { get; set; }
    }
}
