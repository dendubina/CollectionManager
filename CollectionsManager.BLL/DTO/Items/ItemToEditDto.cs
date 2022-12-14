using System;
using System.Collections.Generic;
using CollectionsManager.BLL.DTO.CustomFields;
using CollectionsManager.BLL.DTO.Tags;

namespace CollectionsManager.BLL.DTO.Items
{
    public class ItemToEditDto
    {
        public Guid Id { get; set; }

        public Guid CollectionId { get; set; }

        public string CurrentUserId { get; set; }

        public string Name { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }

        public IList<CustomFieldValueToManipulateDto> CustomFields { get; set; }
    }
}
