using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaHandler : IRequestHandler<UpdatePizzaRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public UpdatePizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(UpdatePizzaRequest request, CancellationToken cancellationToken)
        {
            var pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (pizza is null)
                return  new NotFoundError();

            request.Pizza.Adapt(pizza);

            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
