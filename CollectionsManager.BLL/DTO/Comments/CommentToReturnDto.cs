using System;

namespace CollectionsManager.BLL.DTO.Comments
{
    public class CommentToReturnDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }
    }
}
