using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Toppings.Get
{
    public class GetToppingHandler : IRequestHandler<GetToppingRequest, Topping>
    {
        private readonly ContosoPizzaContext db;

        public GetToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Topping> Handle(GetToppingRequest request, CancellationToken cancellationToken)
        {
            return await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        }
    }
}
