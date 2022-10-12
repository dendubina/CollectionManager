using System;
using CollectionsManager.DAL.Constants;

namespace CollectionsManager.BLL.DTO.CustomFields
{
    public class CustomFieldValueToManipulateDto
    {
        public Guid Id { get; set; }

        public Guid CustomFieldId { get; set; }

        public Guid ItemId { get; set; }

        public string Name { get; set; }

        public CustomPropertyType Type { get; set; }

        public string Value { get; set; }
    }
}
