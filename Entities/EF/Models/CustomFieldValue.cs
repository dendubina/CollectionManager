using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.EF.Models
{
    public class CustomFieldValue
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public Guid? ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }

        public Guid CustomFieldId { get; set; }

        public CustomField Field { get; set; }
    }
}
