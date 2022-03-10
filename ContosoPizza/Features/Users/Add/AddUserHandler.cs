using ContosoPizza.Models;
using Mapster;
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

            var user = request.Adapt<User>();

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),

            db.Users.Add(user);

            await db.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
