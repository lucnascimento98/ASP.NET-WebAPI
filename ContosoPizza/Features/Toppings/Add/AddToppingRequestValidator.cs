using FluentValidation;

namespace ContosoPizza.Features.Toppings.Add
{
    public class AddToppingRequestValidator : AbstractValidator<AddToppingRequest>
    {
        public AddToppingRequestValidator()
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
