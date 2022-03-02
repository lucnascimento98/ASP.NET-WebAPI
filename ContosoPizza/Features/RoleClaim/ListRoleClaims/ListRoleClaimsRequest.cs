using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim.ListRoleClaims
{
    public class ListRoleClaimsRequest : IRequest<ResultOf<List<string>>>
    {
        public int RoleId { get; set; }
    }
}
