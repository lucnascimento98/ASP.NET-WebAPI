using FluentValidation;

namespace ContosoPizza.Features.Users.Add
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email not valid.");

            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Password)
                .NotEmpty();

            RuleFor(p => p.RoleId)
                .NotEmpty();
        }
    }
}
