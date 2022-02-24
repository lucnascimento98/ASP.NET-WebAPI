using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public UpdateUserHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (user is null)
                return new NotFoundError();

            user.Name = request.User.Name;
            user.Email = request.User.Email;

            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
