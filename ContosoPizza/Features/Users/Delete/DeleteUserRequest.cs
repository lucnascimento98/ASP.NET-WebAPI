using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users.Delete
{
    public class DeleteUserRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
