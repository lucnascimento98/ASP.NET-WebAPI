using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingRequest : IRequest<List<ToppingDTO>>
    {
        public string Search { get; set; }
        public int Page { get; set; } = 1;
        public int Quantity { get; set; } = 10;
    }
}
