using ContosoPizza.DTOs;
using MediatR;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public UpdateToppingRequestDTO Topping { get; set; }
    }
}
