using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.EF.Models
{
    public class Tag
    {
        [Required]
        [MaxLength(50)]
        [Key]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
