using CollectionsManager.BLL.DTO.User;
using FluentValidation;

namespace CollectionManager.WEB.Validators.Account
{
    public class SignInModelValidator : AbstractValidator<SignInModel>
    {
        public SignInModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
