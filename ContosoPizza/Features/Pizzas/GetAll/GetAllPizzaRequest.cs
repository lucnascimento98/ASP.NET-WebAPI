using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaRequest : IRequest<List<Pizza>>
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int Quantity { get; set; }
        public bool? GlutenFree { get; set; }
    }
}
