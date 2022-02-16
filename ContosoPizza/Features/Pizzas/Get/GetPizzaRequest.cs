using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaRequest : IRequest<ResultOf<PizzaDTO>>
    {
        public int Id { get; set; }
    }
}
