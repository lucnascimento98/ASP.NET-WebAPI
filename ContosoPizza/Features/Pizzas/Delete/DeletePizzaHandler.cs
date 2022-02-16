using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.Delete
{
    public class DeletePizzaHandler : IRequestHandler<DeletePizzaRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public DeletePizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(DeletePizzaRequest request, CancellationToken cancellationToken)
        {
            var pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id,cancellationToken);
            if (pizza is null)
                return new NotFoundError();

            db.Pizzas.Remove(pizza);
            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
