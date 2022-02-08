using FluentValidation;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequestValidator : AbstractValidator<UpdateToppingRequest>
    {
        public UpdateToppingRequestValidator()
        {
            RuleFor(p => p.Topping.Value)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Value must be positive.");

            RuleFor(p => p.Topping.Name)
                .NotEmpty();
        }
    }
}
