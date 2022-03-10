using ContosoPizza.DTOs;
using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Get
{
    public class GetRoleHandler : IRequestHandler<GetRoleRequest, ResultOf<RoleDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }

        public async Task<ResultOf<RoleDTO>> Handle(GetRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await db.Roles.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (role == null)
                return new NotFoundError();

            return role.Adapt<RoleDTO>();
        }
    }
}
