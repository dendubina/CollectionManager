using CollectionsManager.DAL.Constants;

namespace CollectionsManager.BLL.DTO.CustomFieldValues
{
    public class CustomFieldValueToReturnDto
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CustomPropertyType CustomPropertyType { get; set; }
    }
}
