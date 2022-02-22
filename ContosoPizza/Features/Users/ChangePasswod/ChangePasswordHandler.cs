using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.ChangePasswod
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public ChangePasswordHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }

        public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);
            
            if (user == null)
                return new BadRequestError();

            if (!BCrypt.Net.BCrypt.Verify(request.ChangePasswordRequestDTO.OldPassword, user.PasswordHash))
                return new BadRequestError();

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.ChangePasswordRequestDTO.NewPassword);
            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
            
        }
    }
}
