using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingHandler : IRequestHandler<UpdateToppingRequest, bool>
    {
        private readonly ContosoPizzaContext db;

        public UpdateToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<bool> Handle(UpdateToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (topping is null)
                return false;

            topping.Name = request.Topping.Name;
            topping.Value = request.Topping.Value;

            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
