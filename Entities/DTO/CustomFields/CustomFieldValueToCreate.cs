using System;
using Entities.Constants;

namespace Entities.DTO.CustomFields
{
    public class CustomFieldValueToCreate
    {
        public Guid CustomFieldId { get; set; }

        public string Name { get; set; }

        public CustomPropertyType Type { get; set; }

        public string Value { get; set; }
    }
}
