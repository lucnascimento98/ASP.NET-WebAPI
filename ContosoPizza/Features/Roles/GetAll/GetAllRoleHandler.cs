using ContosoPizza.DTOs;
using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.GetAll
{
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleRequest, ResultOf<PageResult<RoleDTO>>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<PageResult<RoleDTO>>> Handle(GetAllRoleRequest request, CancellationToken cancellationToken)
        {
            var roles = db.Roles.AsQueryable();

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                roles = roles.Where(pizza => pizza.Name.Contains(request.Search));
            }

            var total = await roles.CountAsync(cancellationToken);

            List<RoleDTO> list = await roles.ProjectToType<RoleDTO>().PaginateBy(request, p => p.Name).ToListAsync(cancellationToken);

            return new PageResult<RoleDTO>(request, total, list);
        }
    }
}
