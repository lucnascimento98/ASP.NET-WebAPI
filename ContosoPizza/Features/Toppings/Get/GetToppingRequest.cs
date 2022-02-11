using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingRequest : IRequest<ToppingDTO>
    {
        public int Id { get; set;}
    }
}
