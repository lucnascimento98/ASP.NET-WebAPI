using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim.RemoveClaimFromRole
{
    public class RemoveClaimFromRoleHandler : IRequestHandler<RemoveClaimFromRoleRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public RemoveClaimFromRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(RemoveClaimFromRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await db.Roles
                .Include(d => d.RoleClaims)
                .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

            if (role == null)
                return new NotFoundError().AddFieldErrors(nameof(request.RoleId), "NotFound");

            var claim = role.RoleClaims.FirstOrDefault(d => d.Claim == request.Claim);

            if(claim == null)
                return new NotFoundError().AddFieldErrors(nameof(request.Claim), "NotFound");

            db.RoleClaims.Remove(claim);

            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
