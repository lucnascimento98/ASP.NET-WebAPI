using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Users.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, Result>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UpdateUserHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userContext = httpContextAccessor.HttpContext.User;
            var userClaims = userContext.Claims
                .Where(claim => claim.Type == "Claim")
                .Select(d => Enum.Parse<Claims>(d.Value))
                .ToList();

            if (userContext.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value != request.Id.ToString()
                && !userClaims.Contains(Claims.EditAllUser))
                return new ForbiddenError();

            var userUpdated = await db.Users.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (userUpdated is null)
                return new NotFoundError();

            userUpdated.Name = request.User.Name;
            userUpdated.Email = request.User.Email;

            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
