using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingRequest : IRequest<List<Topping>>
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int Quantity { get; set; }
    }
}
