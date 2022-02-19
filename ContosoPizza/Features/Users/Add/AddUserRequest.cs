using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Add
{
    public class AddUserRequest : IRequest<ResultOf<int>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
