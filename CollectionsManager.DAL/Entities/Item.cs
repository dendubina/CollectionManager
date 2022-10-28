using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollectionsManager.DAL.Entities
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Collection Collection { get; set; }

        public Guid CollectionId { get; set; }

        public DateTime AddedDate { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<CustomFieldValue> CustomValues { get; set; }
    }
}
