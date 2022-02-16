using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingHandler : IRequestHandler<GetToppingRequest, ResultOf<ToppingDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<ToppingDTO>> Handle(GetToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (topping == null) 
                return new NotFoundError();

            return new ToppingDTO()
            {
                Id = topping.Id,
                Name = topping.Name,
                Value = topping.Value
            };
        }
    }
}
