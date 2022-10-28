using CollectionManager.WEB.Validators.CustomFields;
using CollectionsManager.BLL.DTO.Items;
using FluentValidation;

namespace CollectionManager.WEB.Validators.Items
{
    public class ItemToEditValidator : AbstractValidator<ItemToEditDto>
    {
        public ItemToEditValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();

            RuleForEach(x => x.CustomFields)
                .SetValidator(new CustomFieldValueToManipulateValidator());
        }
    }
}
