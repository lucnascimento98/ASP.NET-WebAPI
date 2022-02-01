using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Pizzas.Delete
{
    public class DeletePizzaHandler : IRequestHandler<DeletePizzaRequest, bool>
    {
        private readonly ContosoPizzaContext db;

        public DeletePizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<bool> Handle(DeletePizzaRequest request, CancellationToken cancellationToken)
        {
            var pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id,cancellationToken);
            if (pizza is null)
                return false;

            db.Pizzas.Remove(pizza);
            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
