using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingRequest : IRequest<ResultOf<ToppingDTO>>
    {
        public int Id { get; set;}
    }
}
