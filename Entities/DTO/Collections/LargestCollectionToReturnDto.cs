using System;

namespace Entities.DTO.Collections
{
    public class LargestCollectionToReturnDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public string OwnerId { get; set; }

        public string OwnerName { get; set; }

        public int ItemsCount { get; set; }
    }
}
