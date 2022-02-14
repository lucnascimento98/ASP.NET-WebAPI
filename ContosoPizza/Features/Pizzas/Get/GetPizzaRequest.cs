using ContosoPizza.DTOs;
using MediatR;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaRequest : IRequest<PizzaDTO>
    {
        public int Id { get; set; }
    }
}
