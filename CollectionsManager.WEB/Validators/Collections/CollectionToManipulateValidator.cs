using System.Linq;
using CollectionManager.WEB.Validators.CustomFields;
using CollectionsManager.BLL.DTO.Collections;
using FluentValidation;

namespace CollectionManager.WEB.Validators.Collections
{
    public class CollectionToManipulateValidator : AbstractValidator<CollectionToManipulateDto>
    {
        public CollectionToManipulateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();

            RuleFor(x => x.Category)
                .IsInEnum();

            RuleFor(x => x.Description)
                .NotNull().NotEmpty();

            RuleFor(x => x.CustomFields)
                .Must(x => x.Any());

            RuleForEach(x => x.CustomFields)
                .SetValidator(new CustomFieldToManipulateValidator());
        }
    }
}
