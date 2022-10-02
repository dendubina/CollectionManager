using Entities.Constants;

namespace Entities.DTO.CustomFields
{
    public class CustomFieldToCreate
    {
        public string Name { get; set; }

        public CustomPropertyType Type { get; set; }
    }
}
