using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.EF.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<CustomField> CustomFieldTypes { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public Collection Collection { get; set; }

        public Guid CollectionId { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<CustomFieldValue> CustomFieldsValues { get; set; }
    }
}
