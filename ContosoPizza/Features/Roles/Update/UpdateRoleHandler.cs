using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Update
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public UpdateRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await db.Roles.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (role is null)
                return new NotFoundError();

            request.Adapt(role);

            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
