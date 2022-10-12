using System;

namespace CollectionsManager.BLL.DTO.Comments
{
    public class CommentToCreateDto
    {
        public Guid ItemId { get; set; }

        public string AuthorId { get; set; }

        public string CommentText { get; set; }
    }
}
