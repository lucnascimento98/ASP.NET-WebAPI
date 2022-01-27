using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingRequest : IRequest<Topping>
    {
        public int Id { get; set;}
    }
}
