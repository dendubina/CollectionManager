using System;
using Entities.Constants;

namespace Entities.DTO.CustomFields
{
    public class CustomFieldToManipulateDto
    {
        public Guid Id { get; set; }

        public Guid CollectionId { get; set; }

        public string Name { get; set; }

        public CustomPropertyType Type { get; set; }
    }
}
