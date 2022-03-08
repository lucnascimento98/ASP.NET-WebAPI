using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Users.ChangePasswod
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, Result>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChangePasswordHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var userEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;

            var user = await db.Users.FirstOrDefaultAsync(user => user.Email == userEmail, cancellationToken);

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
                return new BadRequestError();

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
            
        }
    }
}
