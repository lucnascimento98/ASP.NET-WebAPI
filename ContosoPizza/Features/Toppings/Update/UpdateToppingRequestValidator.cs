using FluentValidation;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequestValidator : AbstractValidator<UpdateToppingRequest>
    {
        public UpdateToppingRequestValidator()
        {
            RuleFor(p => p.Value)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Value must be positive.");

            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
