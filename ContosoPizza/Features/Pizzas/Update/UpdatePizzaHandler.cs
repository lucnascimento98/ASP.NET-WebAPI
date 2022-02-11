using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaHandler : IRequestHandler<UpdatePizzaRequest, bool>
    {
        private readonly ContosoPizzaContext db;

        public UpdatePizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<bool> Handle(UpdatePizzaRequest request, CancellationToken cancellationToken)
        {
            var pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (pizza is null)
                return false;

            pizza.Name = request.Pizza.Name;
            pizza.IsGlutenFree = request.Pizza.IsGlutenFree;
            pizza.Value = request.Pizza.Value;

            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
