using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.ChangePasswod
{
    public class ChangePasswordRequest : IRequest<Result>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
