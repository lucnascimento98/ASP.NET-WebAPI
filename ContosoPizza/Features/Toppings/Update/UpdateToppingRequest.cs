using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public UpdateToppingRequestDTO Topping { get; set; }
    }
}
