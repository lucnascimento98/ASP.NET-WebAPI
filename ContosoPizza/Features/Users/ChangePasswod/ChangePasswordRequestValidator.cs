using FluentValidation;

namespace ContosoPizza.Features.Users.ChangePasswod
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(p => p.NewPassword)
                .NotEmpty();

            RuleFor(p => p.OldPassword)
                .NotEmpty();
        }
    }
}
