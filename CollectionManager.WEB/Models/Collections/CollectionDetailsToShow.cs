using System;
using System.Collections.Generic;
using CollectionManager.WEB.Models.Items;
using Entities.Constants;

namespace CollectionManager.WEB.Models.Collections
{
    public class CollectionDetailsToShow
    {
        public Guid Id { get; set; }

        public string Name  { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }

        public CollectionCategory Category { get; set; }

        public IEnumerable<ItemInCollectionDetails> ItemInCollectionDetails { get; set; }   
    }
}
