using System;
using System.Collections.Generic;
using CollectionsManager.BLL.DTO.CustomFields;
using CollectionsManager.DAL.Constants;
using Microsoft.AspNetCore.Http;

namespace CollectionsManager.BLL.DTO.Collections
{
    public class CollectionToManipulateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CollectionCategory Category { get; set; }

        public string OwnerId { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public IList<CustomFieldToManipulateDto> CustomFields { get; set; }
    }
}
