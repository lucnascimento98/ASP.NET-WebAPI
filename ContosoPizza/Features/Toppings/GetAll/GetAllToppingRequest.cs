using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingRequest :PageRequest,  IRequest<ResultOf<PageResult<ToppingDTO>>>
    {
        public string Search { get; set; }
    }
}
