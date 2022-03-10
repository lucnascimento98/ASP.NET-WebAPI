using FluentValidation;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaRequestValidator : AbstractValidator<GetPizzaRequest>
    {
        public GetPizzaRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
