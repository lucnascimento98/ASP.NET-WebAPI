using FluentValidation;

namespace ContosoPizza.Features.RoleClaim.AddClaimToRole
{
    public class AddClaimToRoleRequestValidator : AbstractValidator<AddClaimToRoleRequest>
    {
        public AddClaimToRoleRequestValidator()
        {
            RuleFor(d => d.Claim)
                .IsInEnum();
        }
    }
}
