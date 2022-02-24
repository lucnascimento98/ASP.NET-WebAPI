using ContosoPizza.Models;
using MediatR;
using Nudes.Retornator.Core;
using Nudes.Retornator.AspnetCore.Errors;
using ContosoPizza.DTOs;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Services;

namespace ContosoPizza.Features.Authentication.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, ResultOf<AuthenticationResult>>
    {
        private readonly ContosoPizzaContext db;
        private readonly TokenService tokenService;


        public LoginHandler(ContosoPizzaContext db, TokenService tokenService)
        {
            this.tokenService = tokenService;
            this.db = db;
        }
        public async Task<ResultOf<AuthenticationResult>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
                return new UnauthorizedError();

            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return new UnauthorizedError();

            return new AuthenticationResult()
            {
                AccessToken = tokenService.GenerateToken(user),
            };
        }
    }
}
