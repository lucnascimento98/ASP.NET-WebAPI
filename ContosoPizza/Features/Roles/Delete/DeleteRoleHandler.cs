using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Delete
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public DeleteRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await db.Roles.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (role is null)
                return new NotFoundError();

            db.Roles.Remove(role);
            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
