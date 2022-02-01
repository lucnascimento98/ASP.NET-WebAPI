using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaRequest : IRequest<List<Pizza>>
    {
        public string Search { get; set; }
        public int Page { get; set; } = 1;
        public int Quantity { get; set; } = 10; 
        public bool? GlutenFree { get; set; }
    }
}
