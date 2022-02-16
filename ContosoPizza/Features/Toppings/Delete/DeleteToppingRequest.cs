using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Delete
{
    public class DeleteToppingRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
