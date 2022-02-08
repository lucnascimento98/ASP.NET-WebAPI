using FluentValidation;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaRequestValidator : AbstractValidator<UpdatePizzaRequest>
    {
        public UpdatePizzaRequestValidator()
        {
            RuleFor(p => p.Pizza.Value)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Value must be positive.");

            RuleFor(p => p.Pizza.Name)
                .NotEmpty();
        }
    }
}
