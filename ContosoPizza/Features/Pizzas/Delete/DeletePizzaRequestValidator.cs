using FluentValidation;

namespace ContosoPizza.Features.Pizzas.Delete
{
    public class DeletePizzaRequestValidator : AbstractValidator<DeletePizzaRequest>
    {
        public DeletePizzaRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
