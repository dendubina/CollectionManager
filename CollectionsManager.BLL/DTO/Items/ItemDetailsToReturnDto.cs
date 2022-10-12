using System;
using System.Collections.Generic;
using CollectionsManager.BLL.DTO.Comments;
using CollectionsManager.BLL.DTO.CustomFieldValues;
using CollectionsManager.BLL.DTO.Likes;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.DTO.Items
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
