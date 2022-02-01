using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public Topping Topping { get; set; }
    }
}
