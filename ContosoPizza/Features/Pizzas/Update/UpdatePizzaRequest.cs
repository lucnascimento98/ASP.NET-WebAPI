using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public Pizza Pizza { get; set; }
    }
}
