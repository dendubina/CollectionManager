using CollectionsManager.BLL.DTO.CustomFields;
using FluentValidation;

namespace CollectionManager.WEB.Validators.CustomFields
{
    public class CustomFieldToManipulateValidator : AbstractValidator<CustomFieldToManipulateDto>
    {
        public CustomFieldToManipulateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}
