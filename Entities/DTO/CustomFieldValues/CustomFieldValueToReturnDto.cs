using Entities.Constants;

namespace Entities.DTO.CustomFieldValues
{
    public class CustomFieldValueToReturnDto
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CustomPropertyType CustomPropertyType { get; set; }
    }
}
