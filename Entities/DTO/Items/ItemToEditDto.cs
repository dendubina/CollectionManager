using System;
using System.Collections.Generic;
using Entities.DTO.CustomFields;

namespace Entities.DTO.Items
{
    public class ItemToEditDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<CustomFieldValueToManipulateDto> CustomFields { get; set; }
    }
}
