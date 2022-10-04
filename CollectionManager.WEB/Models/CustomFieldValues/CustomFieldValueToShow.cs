using Entities.Constants;

namespace CollectionManager.WEB.Models.CustomFieldValues
{
    public class CustomFieldValueToShow
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CustomPropertyType CustomPropertyType { get; set; }
    }
}
