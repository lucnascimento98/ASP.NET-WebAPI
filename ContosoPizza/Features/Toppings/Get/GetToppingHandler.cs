using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingHandler : IRequestHandler<GetToppingRequest, ToppingDTO>
    {
        private readonly ContosoPizzaContext db;

        public GetToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ToppingDTO> Handle(GetToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (topping == null) 
                return null;

            return new ToppingDTO()
            {
                Id = topping.Id,
                Name = topping.Name,
                Value = topping.Value
            };
        }
    }
}
