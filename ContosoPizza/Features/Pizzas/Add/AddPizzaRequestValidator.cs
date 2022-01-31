using FluentValidation;

namespace ContosoPizza.Features.Pizzas.Add
{
    public class AddPizzaRequestValidator : AbstractValidator<AddPizzaRequest>
    {
        public AddPizzaRequestValidator()
        {
            RuleFor(p => p.Value)
                .NotEmpty();

            RuleFor(p => p.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Value must be positive.");

            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
