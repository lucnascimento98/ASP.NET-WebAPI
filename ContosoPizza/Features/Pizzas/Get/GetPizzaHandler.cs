using ContosoPizza.DTOs;
using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaHandler : IRequestHandler<GetPizzaRequest, ResultOf<PizzaDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetPizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<PizzaDTO>> Handle(GetPizzaRequest request, CancellationToken cancellationToken)
        {
            var pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (pizza == null)
                return new NotFoundError();

            return pizza.Adapt<PizzaDTO>();
        }
    }
}
