using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.EF.Models
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
