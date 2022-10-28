using System;
using System.Collections.Generic;

namespace CollectionsManager.BLL.Services.SearchService.Models
{
    public class SearchItem
    {
        public Guid Id { get; set; }

        public string CollectionName { get; set; }

        public string OwnerName { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<CustomField> CustomFields { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
