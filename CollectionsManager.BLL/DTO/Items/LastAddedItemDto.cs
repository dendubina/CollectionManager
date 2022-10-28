using System;

namespace CollectionsManager.BLL.DTO.Items
{
    public class LastAddedItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CollectionId { get; set; }

        public string AuthorName { get; set; }
    }
}
