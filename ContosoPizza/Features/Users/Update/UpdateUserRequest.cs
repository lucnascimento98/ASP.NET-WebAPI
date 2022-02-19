using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Update
{
    public class UpdateUserRequest :IRequest<Result>
    {
        public int Id { get; set; }
        public UpdateUserRequestDTO User { get; set; }
    }
}
