using FluentValidation;

namespace ContosoPizza.Features.Roles.Delete
{
    public class DeleteRoleRequestValidator : AbstractValidator<DeleteRoleRequest>
    {
        public DeleteRoleRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
