using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.Comments
{
    public class CommentToCreateDto
    {
        public Guid ItemId { get; set; }

        public string CommentText { get; set; }
    }
}
