using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Get
{
    public class GetRoleRequest : IRequest<ResultOf<RoleDTO>>
    {
        public int Id { get; set; }
    }
}
