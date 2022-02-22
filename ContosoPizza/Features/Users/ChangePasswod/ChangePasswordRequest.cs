using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.ChangePasswod
{
    public class ChangePasswordRequest : IRequest<Result>
    {
        public int Id { get; set; }

        public ChangePasswordRequestDTO ChangePasswordRequestDTO { get; set; }
    }
}
