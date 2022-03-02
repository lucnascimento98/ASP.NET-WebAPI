using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim.ListRoleClaims
{
    public class ListRoleClaimsHandler : IRequestHandler<ListRoleClaimsRequest, ResultOf<List<string>>>
    {
        private readonly ContosoPizzaContext db;

        public ListRoleClaimsHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }

        public async Task<ResultOf<List<string>>> Handle(ListRoleClaimsRequest request, CancellationToken cancellationToken)
        {
            var role = await db.Roles.Include(d => d.RoleClaims).FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);
            return role == null
                ? new NotFoundError()
                : role.RoleClaims.Select(p => p.Claim.ToString()).ToList();
        }
    }
}
