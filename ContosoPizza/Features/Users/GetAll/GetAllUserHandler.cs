using ContosoPizza.DTOs;
using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.GetAll
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, ResultOf<PageResult<UserDTO>>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllUserHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<PageResult<UserDTO>>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var user = db.Users.Include(d => d.Role).AsQueryable();

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                user = user.Where(u => u.Name.Contains(request.Search));
            }

            if (request.RoleId != null)
            {
                user = user.Where(u => u.RoleId == request.RoleId);
            }

            var total = await user.CountAsync(cancellationToken);

            List<UserDTO> list = await user.ProjectToType<UserDTO>().PaginateBy(request, p => p.Name).ToListAsync(cancellationToken);

            return new PageResult<UserDTO>(request, total, list);
        }
    }
}
