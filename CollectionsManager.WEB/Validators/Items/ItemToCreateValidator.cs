using CollectionManager.WEB.Validators.CustomFields;
using CollectionsManager.BLL.DTO.Items;
using FluentValidation;

namespace CollectionManager.WEB.Validators.Items
{
    public class ItemToCreateValidator : AbstractValidator<ItemToCreate>
    {
        public ItemToCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();

            RuleForEach(x => x.CustomFieldValuesToCreate)
                .SetValidator(new CustomFieldValueToManipulateValidator());
        }
    }
}
