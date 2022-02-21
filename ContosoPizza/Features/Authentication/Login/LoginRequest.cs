using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Authentication.Login
{
    public class LoginRequest : IRequest<ResultOf<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
