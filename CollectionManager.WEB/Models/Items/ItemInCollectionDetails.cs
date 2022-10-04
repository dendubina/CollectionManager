using System;
using System.Collections.Generic;
using CollectionManager.WEB.Models.CustomFieldValues;

namespace CollectionManager.WEB.Models.Items
{
    public class ItemInCollectionDetails
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public IEnumerable<CustomFieldValueToShow> CustomFieldValues { get; set; }
    }
}
