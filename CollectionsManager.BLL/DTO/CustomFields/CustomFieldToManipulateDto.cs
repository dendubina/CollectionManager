using System;
using CollectionsManager.DAL.Constants;

namespace CollectionsManager.BLL.DTO.CustomFields
{
    public class CustomFieldToManipulateDto
    {
        public Guid Id { get; set; }

        public Guid CollectionId { get; set; }

        public string Name { get; set; }

        public bool ToRemove { get; set; }

        public CustomPropertyType Type { get; set; }
    }
}
