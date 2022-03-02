using ContosoPizza.Features.RoleClaim.AddClaimToRole;
using ContosoPizza.Features.RoleClaim.ListRoleClaims;
using ContosoPizza.Features.RoleClaim.RemoveClaimFromRole;
using ContosoPizza.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.RoleClaim
{
    [ApiController]
    [Route("[controller]")]
    public class RoleClaimController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleClaimController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{RoleId}")]
        public Task<ResultOf<List<string>>> ListRoleClaims ([FromRoute] ListRoleClaimsRequest listRoleClaimsRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(listRoleClaimsRequest, cancellationToken);
        }

        [HttpDelete]
        public Task<Result> RemoveClaimFromRole ([FromQuery] RemoveClaimFromRoleRequest removeClaimFromRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(removeClaimFromRoleRequest, cancellationToken);
        }

        [HttpPost]
        public Task<ResultOf<int>> AddClaimToRole([FromBody] AddClaimToRoleRequest addClaimToRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(addClaimToRoleRequest, cancellationToken);
        }
    }
}
