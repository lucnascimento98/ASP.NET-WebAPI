using FluentValidation;

namespace ContosoPizza.Features.Roles.Get
{
    public class GetRoleRequestValidator : AbstractValidator<GetRoleRequest>
    {
        public GetRoleRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
