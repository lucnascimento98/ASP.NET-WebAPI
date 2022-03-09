
using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using ContosoPizza.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim.AddClaimToRole
{
    public class AddClaimToRoleHandler : IRequestHandler<AddClaimToRoleRequest, ResultOf<int>>
    {
        private readonly ContosoPizzaContext db;

        public AddClaimToRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }

        public async Task<ResultOf<int>> Handle(AddClaimToRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await db.Roles.Include(d => d.RoleClaims).FirstOrDefaultAsync(p => p.Id == request.RoleId, cancellationToken);

            if (role == null)
                return new NotFoundError().AddFieldErrors(nameof(request.RoleId), "NotFound");

            if (role.RoleClaims.Any(d => d.Claim == request.Claim))
                return new BadRequestError().AddFieldErrors(nameof(request.Claim), "This role already has this claim");

            var roleClaim = new Models.RoleClaim()
            {
                RoleId = request.RoleId,
                Claim = request.Claim,
            };

            db.RoleClaims.Add(roleClaim);

            await db.SaveChangesAsync(cancellationToken);

            return roleClaim.Id;
        }
    }
}
