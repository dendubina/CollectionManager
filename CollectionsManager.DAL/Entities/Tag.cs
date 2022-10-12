using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollectionsManager.DAL.Entities
{
    public class Tag
    {
        [Required]
        [MaxLength(50)]
        [Key]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }

        public override bool Equals(object obj)
            => obj is Tag tag && Name == tag.Name;

        public override int GetHashCode()
            => Name.GetHashCode();
    }
}
