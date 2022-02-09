using FluentValidation;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaRequestValidator : AbstractValidator<UpdatePizzaRequest>
    {
        public UpdatePizzaRequestValidator()
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
