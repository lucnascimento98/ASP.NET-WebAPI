using FluentValidation;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingRequestValidator : AbstractValidator<GetToppingRequest>
    {
        public GetToppingRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
