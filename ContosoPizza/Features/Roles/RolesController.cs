using ContosoPizza.DTOs;
using ContosoPizza.Features.Roles.Add;
using ContosoPizza.Features.Roles.Delete;
using ContosoPizza.Features.Roles.Get;
using ContosoPizza.Features.Roles.GetAll;
using ContosoPizza.Features.Roles.Update;
using MediatR;
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

        [HttpGet]
        public Task<ResultOf<PageResult<RoleDTO>>> GetAll([FromQuery] GetAllRoleRequest getAllRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllRoleRequest, cancellationToken);
        }

        [HttpGet("{Id}")]
        public Task<ResultOf<RoleDTO>> Get([FromRoute] GetRoleRequest getRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getRoleRequest, cancellationToken);
        }

        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] AddRoleRequest request, CancellationToken cancellationToken)
        {
            return _mediator.Send(request, cancellationToken);

        }

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

        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeleteRoleRequest deleteRoleRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(deleteRoleRequest, cancellationToken);
        }
    }
}
