using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaRequest : PageRequest, IRequest<PageResult<PizzaDTO>>
    {
        public string Search { get; set; }
        public bool? GlutenFree { get; set; }
    }
}
