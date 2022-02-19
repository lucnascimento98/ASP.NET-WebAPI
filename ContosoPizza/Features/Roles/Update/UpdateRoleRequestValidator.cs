using FluentValidation;

namespace ContosoPizza.Features.Roles.Update
{
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(r => r.Role.Name)
                .NotEmpty();
        }
    }
}
