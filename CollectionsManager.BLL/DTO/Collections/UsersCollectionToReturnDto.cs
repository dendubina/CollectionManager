using System;
using CollectionsManager.DAL.Constants;

namespace CollectionsManager.BLL.DTO.Collections
{
    public class UsersCollectionToReturnDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CollectionCategory Category { get; set; }

        public string ImageSource { get; set; }

        public int ItemsCount { get; set; }
    }
}
