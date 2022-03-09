using ContosoPizza.DTOs;
using ContosoPizza.Features.Roles.Add;
using ContosoPizza.Features.Roles.Delete;
using ContosoPizza.Features.Roles.Get;
using ContosoPizza.Features.Roles.GetAll;
using ContosoPizza.Features.Roles.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "GetAllRole")]
        [HttpGet]
        public Task<ResultOf<PageResult<RoleDTO>>> GetAll([FromQuery] GetAllRoleRequest getAllRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllRoleRequest, cancellationToken);
        }

        [Authorize(Policy = "GetRole")]
        [HttpGet("{Id}")]
        public Task<ResultOf<RoleDTO>> Get([FromRoute] GetRoleRequest getRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getRoleRequest, cancellationToken);
        }

        [Authorize(Policy = "AddRole")]
        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] AddRoleRequest addRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(addRoleRequest, cancellationToken);

        }

        [Authorize(Policy = "EditRole")]
        [HttpPut("{id}")]
        public Task<Result> Update(int id, [FromBody] UpdateRoleRequestDTO roleDTO, CancellationToken cancellationToken)
        {
            UpdateRoleRequest updateRoleRequest = new()
            {
                Id = id,
                Role = roleDTO
            };
            return _mediator.Send(updateRoleRequest, cancellationToken);
        }

        [Authorize(Policy = "DeleteRole")]
        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeleteRoleRequest deleteRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(deleteRoleRequest, cancellationToken);
        }
    }
}
