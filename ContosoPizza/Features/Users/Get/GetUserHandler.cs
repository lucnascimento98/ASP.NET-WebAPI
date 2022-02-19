using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Get
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, ResultOf<UserDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetUserHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<UserDTO>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (user == null)
                return new NotFoundError();

            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }
    }
}
