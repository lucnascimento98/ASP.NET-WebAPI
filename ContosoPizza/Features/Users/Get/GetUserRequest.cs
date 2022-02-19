using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Get
{
    public class GetUserRequest : IRequest<ResultOf<UserDTO>>
    {
        public int Id { get; set; }
    }
}
