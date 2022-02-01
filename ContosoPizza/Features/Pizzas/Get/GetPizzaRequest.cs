using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaRequest : IRequest<Pizza>
    {
        public int Id { get; set; }
    }
}
