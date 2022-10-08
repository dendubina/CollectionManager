using System;
using System.Collections.Generic;
using Entities.DTO.CustomFields;
using Entities.EF.Models;

namespace Entities.DTO.Items
{
    public class ItemToCreate
    {
        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IList<CustomFieldValueToManipulateDto> CustomFieldValuesToCreate { get; set; }

        public IList<Tag> Tags { get; set; }
    }
}
