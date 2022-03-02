using ContosoPizza.Models.Enums;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim.RemoveClaimFromRole
{
    public class RemoveClaimFromRoleRequest : IRequest<Result>
    {
        public int RoleId { get; set; }
        public Claims Claim { get; set; }
    }
}
