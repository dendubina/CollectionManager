using System;

namespace CollectionsManager.BLL.DTO.Likes
{
    public class LikeToReturnDto
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public string AuthorId { get; set; }
    }
}
