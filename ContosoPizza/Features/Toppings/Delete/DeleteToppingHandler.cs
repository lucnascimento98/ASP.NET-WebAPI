using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Delete
{
    public class DeleteToppingHandler : IRequestHandler<DeleteToppingRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public DeleteToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(DeleteToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = await db.Toppings.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (topping is null)
                return new NotFoundError();

            db.Toppings.Remove(topping);
            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
