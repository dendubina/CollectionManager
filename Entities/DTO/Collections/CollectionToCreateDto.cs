using System.Collections.Generic;
using Entities.Constants;
using Entities.DTO.CustomFields;
using Microsoft.AspNetCore.Http;

namespace Entities.DTO.Collections
{
    public class CollectionToCreateDto
    {
        public string Name { get; set; }

        public CollectionCategory Category { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<CustomFieldToCreate> CustomFields { get; set; }
    }
}
