﻿using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Users.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, Result>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;


        public DeleteUserHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userContext = httpContextAccessor.HttpContext.User;
            var userClaims = userContext.Claims
                .Where(claim => claim.Type == "Claim")
                .Select(d => d.ToString())
                .ToList();

            if (userContext.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).ToString() != request.Id.ToString()
                && !userClaims.Contains("Claim: DeleteAllUser"))
                return new ForbiddenError();

            var userDeleted = await db.Users.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (userDeleted is null)
                return new NotFoundError();


            db.Users.Remove(userDeleted);
            await db.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
