﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.EF.Models
{
    public class Like
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
