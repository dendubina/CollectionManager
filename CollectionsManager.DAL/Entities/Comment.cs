using System;
using System.ComponentModel.DataAnnotations;

namespace CollectionsManager.DAL.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }

        public Item Item { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public Guid ItemId { get; set; }
    }
}
