﻿using System;
using System.ComponentModel.DataAnnotations;
using Entities.Constants;

namespace Entities.EF.Models
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
    }
}
