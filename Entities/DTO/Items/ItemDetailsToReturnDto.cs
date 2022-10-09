using System;
using System.Collections.Generic;
using Entities.DTO.Comments;
using Entities.DTO.CustomFieldValues;
using Entities.DTO.Likes;
using Entities.EF.Models;

namespace Entities.DTO.Items
{
    public class ItemDetailsToReturnDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CollectionName { get; set; }

        public string OwnerName { get; set; }

        public Guid CollectionId { get; set; }

        public IEnumerable<LikeToReturnDto> Likes { get; set; }

        public IEnumerable<CommentToReturnDto> Comments { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<CustomFieldValueToReturnDto> CustomFieldValues { get; set; }
    }
}
