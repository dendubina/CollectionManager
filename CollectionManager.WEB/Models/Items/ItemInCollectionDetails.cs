using System;
using System.Collections.Generic;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.Items
{
    public class ItemInCollectionDetails
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IEnumerable<CustomFieldValue> CustomFieldValues { get; set; }
    }
}
