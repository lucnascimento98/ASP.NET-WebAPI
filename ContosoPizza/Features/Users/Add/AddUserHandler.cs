using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Users.Add
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, ResultOf<int>>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AddUserHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultOf<int>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var UserContext = httpContextAccessor.HttpContext.User;
            var roleId = UserContext.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

            if (!await db.Roles.AnyAsync(role => role.Id == request.RoleId, cancellationToken)) //se o role da request nao existe
            {
                return new BadRequestError();
            }
            var idCliente = (await db.Roles.FirstOrDefaultAsync(d => d.Name == "cliente", cancellationToken)).Id;

            if (roleId == null || roleId.Value == idCliente.ToString()) //se o usuaro for anonimo ou um cliente
            {
                if (request.RoleId != idCliente) //se a role da request nao for cliente 
                    return new UnauthorizedError();
            }
            
            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.RoleId.Value,
            };

            db.Users.Add(user);

            await db.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
