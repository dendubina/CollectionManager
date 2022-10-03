using Entities.Constants;
using System;

namespace CollectionManager.WEB.Models.Collections
{
    public class UsersCollectionToShow
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CollectionCategory Category { get; set; }

        public string ImageSource { get; set; }

        public int ItemsCount { get; set; }
    }
}
