using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Toppings.Delete
{
    public class DeleteToppingHandler : IRequestHandler<DeleteToppingRequest, bool>
    {
        private readonly ContosoPizzaContext db;

        public DeleteToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<bool> Handle(DeleteToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (topping is null)
                return false;

            db.Toppings.Remove(topping);
            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
