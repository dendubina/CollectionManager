using System;
using System.Collections.Generic;

namespace CollectionsManager.BLL.DTO.Items
{
    public class FoundItemToReturnDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public string CollectionName { get; set; }

        public string OwnerName { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
