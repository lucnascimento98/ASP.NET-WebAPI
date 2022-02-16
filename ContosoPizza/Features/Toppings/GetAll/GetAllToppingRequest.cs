using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingRequest :PageRequest,  IRequest<PageResult<ToppingDTO>>
    {
        public string Search { get; set; }
    }
}
