using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.EF.Models
{
    public class CustomFieldValue
    {
        public Guid Id { get; set; }

        [Required]
        public string Value { get; set; }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        public Guid CustomFieldId { get; set; }

        public CustomField CustomField { get; set; }
    }
}
