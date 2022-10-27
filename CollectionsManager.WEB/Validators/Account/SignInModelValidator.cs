using CollectionsManager.BLL.DTO.User;
using FluentValidation;

namespace CollectionManager.WEB.Validators.Account
{
    public class SignInModelValidator : AbstractValidator<SignInModel>
    {
        public SignInModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
