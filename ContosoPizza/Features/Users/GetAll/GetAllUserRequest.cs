using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.GetAll
{
    public class GetAllUserRequest :PageRequest, IRequest<ResultOf<PageResult<UserDTO>>>
    {
        public string Search { get; set; }
    }
}
