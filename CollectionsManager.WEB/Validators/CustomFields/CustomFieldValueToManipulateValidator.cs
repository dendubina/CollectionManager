using System;
using CollectionsManager.BLL.DTO.CustomFields;
using CollectionsManager.DAL.Constants;
using FluentValidation;

namespace CollectionManager.WEB.Validators.CustomFields
{
    public class CustomFieldValueToManipulateValidator : AbstractValidator<CustomFieldValueToManipulateDto>
    {
        public CustomFieldValueToManipulateValidator()
        {

            RuleFor(x => x.Type)
                .IsInEnum()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Value)
                        .Must(x => int.TryParse(x, out _))
                        .When(model => model.Value is not null)
                        .When(model => model.Type == CustomPropertyType.Int)
                        .WithMessage("Invalid int value");

                    RuleFor(x => x.Value)
                        .Must(x => DateTime.TryParse(x, out _))
                        .When(model => model.Type == CustomPropertyType.DateTime)
                        .WithMessage("Please, select date");

                    RuleFor(x => x.Value)
                        .Must(x => bool.TryParse(x, out _))
                        .When(model => model.Type == CustomPropertyType.Bool)
                        .WithMessage("Invalid bool value");
                });
        }
    }
}
