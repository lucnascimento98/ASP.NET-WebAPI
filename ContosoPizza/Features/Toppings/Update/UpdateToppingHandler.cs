using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingHandler : IRequestHandler<UpdateToppingRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public UpdateToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(UpdateToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (topping is null)
                return new NotFoundError();

            request.Adapt(topping);

            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
