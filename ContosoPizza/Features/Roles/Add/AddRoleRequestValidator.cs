using FluentValidation;

namespace ContosoPizza.Features.Roles.Add
{
    public class AddRoleRequestValidator : AbstractValidator<AddRoleRequest>
    {
        public AddRoleRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
