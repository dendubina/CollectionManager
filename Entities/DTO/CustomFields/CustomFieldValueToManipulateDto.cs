using System;
using Entities.Constants;

namespace Entities.DTO.CustomFields
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
