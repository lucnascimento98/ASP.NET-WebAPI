using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaRequest : PageRequest, IRequest<ResultOf<PageResult<PizzaDTO>>>
    {
        public string Search { get; set; }
        public bool? GlutenFree { get; set; }
    }
}
