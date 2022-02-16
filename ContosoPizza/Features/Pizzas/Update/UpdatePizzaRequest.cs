using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public UpdatePizzaRequestDTO Pizza { get; set; }
    }
}
