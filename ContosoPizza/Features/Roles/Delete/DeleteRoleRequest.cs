using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Delete
{
    public class DeleteRoleRequest :IRequest<Result>
    {
        public int Id { get; set; }
    }
}
