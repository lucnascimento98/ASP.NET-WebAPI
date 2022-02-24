using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Update
{
    public class UpdateRoleRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public UpdateRoleRequestDTO Role { get; set; }
    }
}
