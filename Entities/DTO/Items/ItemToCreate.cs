using System;
using System.Collections.Generic;
using Entities.DTO.CustomFields;

namespace Entities.DTO.Items
{
    public class ItemToCreate
    {
        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IList<CustomFieldValueToCreate> CustomFieldValuesToCreate { get; set; }
    }
}
