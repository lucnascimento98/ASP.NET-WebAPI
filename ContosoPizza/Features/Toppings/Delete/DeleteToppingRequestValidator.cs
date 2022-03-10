using FluentValidation;

namespace ContosoPizza.Features.Toppings.Delete
{
    public class DeleteToppingRequestValidator : AbstractValidator<DeleteToppingRequest>
    {
        public DeleteToppingRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
