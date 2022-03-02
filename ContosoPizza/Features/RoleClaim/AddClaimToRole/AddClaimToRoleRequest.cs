using ContosoPizza.Models.Enums;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim.AddClaimToRole
{
    public class AddClaimToRoleRequest : IRequest<ResultOf<int>>
    {
        public int RoleId { get; set; }
        public Claims Claim { get; set; }
    }
}
