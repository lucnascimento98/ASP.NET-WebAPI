using ContosoPizza.Models;
using MediatR;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Add
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, ResultOf<int>>
    {
        private readonly ContosoPizzaContext db;

        public AddUserHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }

        public async Task<ResultOf<int>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            if (!db.Roles.Any(role => role.Id == request.RoleId))
            {
                return new BadRequestError();
            }
            
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
