using ContosoPizza.DTOs;
using MediatR;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public UpdatePizzaRequestDTO Pizza { get; set; }
    }
}
