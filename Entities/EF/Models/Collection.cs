using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Constants;

namespace Entities.EF.Models
{
    public class Collection
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public CollectionCategory Category { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public User Owner { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<CustomField> CustomFields { get; set; }
    }
}
