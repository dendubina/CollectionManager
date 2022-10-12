using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CollectionsManager.DAL.Constants;

namespace CollectionsManager.DAL.Entities
{
    public class CustomField
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public CustomPropertyType FieldType { get; set; }

        public Guid CollectionId { get; set; }

        public Collection Collection { get; set; }

        public ICollection<CustomFieldValue> CustomFieldValues { get; set; }
    }
}
