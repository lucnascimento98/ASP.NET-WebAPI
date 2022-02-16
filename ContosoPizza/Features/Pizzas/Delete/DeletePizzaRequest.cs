using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas
{
    public class DeletePizzaRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
