using CollectionsManager.BLL.DTO.User;
using FluentValidation;

namespace CollectionManager.WEB.Validators.Account
{
    public class SignUpModelValidator : AbstractValidator<SignUpModel>
    {
        public SignUpModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull().NotEmpty();

            RuleFor(x => x.Email)
                .NotNull().NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull().NotEmpty();

            RuleFor(x => x.PasswordConfirm)
                .NotNull().NotEmpty()
                .Equal(x => x.Password);
        }
    }
}
