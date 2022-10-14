using System;
using System.Collections.Generic;
using CollectionManager.WEB.Models.Tags;
using CollectionsManager.BLL.DTO.CustomFields;
using CollectionsManager.BLL.DTO.Tags;

namespace CollectionManager.WEB.Models.Items
{
    public class ItemToEditViewModel
    {
        public Guid Id { get; set; }

        public Guid CollectionId { get; set; }

        public string CurrentUserId { get; set; }

        public string Name { get; set; }

        public IList<TagDto> TagsToAdd { get; set; }

        public IList<ExistedTagToEditViewModel> ExistedTags { get; set; }

        public IList<CustomFieldValueToManipulateDto> CustomFields { get; set; }
    }
}
