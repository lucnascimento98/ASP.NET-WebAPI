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

            if (!await db.Roles.AnyAsync(role => role.Id == request.RoleId, cancellationToken)) //se o role da request nao existe
            {
                return new NotFoundError().AddFieldErrors(nameof(request.RoleId), "NotFound");
            }

            //var userContext = httpContextAccessor.HttpContext.User;
            //var roleId = userContext.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

            //var funcionarioId = (await db.Roles.FirstOrDefaultAsync(d => d.Name == "Funcionario", cancellationToken)).Id;

            //if (roleId.Value != funcionarioId.ToString() && request.RoleId == funcionarioId) //se o usuaro nao for um funcionario e estiver tentando adicionar um funcionario
            //    return new ForbiddenError();

            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.RoleId,
            };

            db.Users.Add(user);

            await db.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
