using ContosoPizza.DTOs;
using ContosoPizza.Models;
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
                user = user.Where(pizza => pizza.Name.Contains(request.Search));
            }

            var total = await user.CountAsync(cancellationToken);

            List<UserDTO> list = await user.Select(p => new UserDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Role = p.Role.Name,
            }).PaginateBy(request, p => p.Name).ToListAsync(cancellationToken);


            return new PageResult<UserDTO>(request, total, list);
        }
    }
}
