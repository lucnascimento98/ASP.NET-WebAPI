using ContosoPizza.DTOs;
using ContosoPizza.Features.Users.Add;
using ContosoPizza.Features.Users.ChangePasswod;
using ContosoPizza.Features.Users.Delete;
using ContosoPizza.Features.Users.Get;
using ContosoPizza.Features.Users.GetAll;
using ContosoPizza.Features.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "GetAllUser")]
        [HttpGet]
        public Task<ResultOf<PageResult<UserDTO>>> GetAll([FromQuery] GetAllUserRequest getAllUserRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllUserRequest, cancellationToken);
        }

        [Authorize(Policy = "GetUser")]
        [HttpGet("{Id}")]
        public Task<ResultOf<UserDTO>> Get([FromRoute] GetUserRequest getUserRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getUserRequest, cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("Client")]
        public Task<ResultOf<int>> CreateClient([FromBody] AddClientRequestDTO addClientRequestDTO, CancellationToken cancellationToken)
        {
            AddUserRequest addUserRequest = new()
            {
                Name = addClientRequestDTO.Name,
                Email = addClientRequestDTO.Email,
                Password = addClientRequestDTO.Password,
                RoleId = 2
            };
            return _mediator.Send(addUserRequest, cancellationToken);
        }

        [Authorize(Policy = "AddUserAdmin")]
        [HttpPost("Admin")]
        public Task<ResultOf<int>> CreateAdmin([FromBody] AddAdminRequestDTO addAdminRequestDTO, CancellationToken cancellationToken)
        {
            AddUserRequest addUserRequest = new()
            {
                Name = addAdminRequestDTO.Name,
                Email = addAdminRequestDTO.Email,
                Password = addAdminRequestDTO.Password,
                RoleId = addAdminRequestDTO.RoleId
            };
            return _mediator.Send(addUserRequest, cancellationToken);
        }

        [Authorize]
        [HttpPut("{id}")]
        public Task<Result> Update(int id, [FromBody] UpdateUserRequestDTO UserDTO, CancellationToken cancellationToken)
        {
            UpdateUserRequest updateUserRequest = new()
            {
                Id = id,
                User = UserDTO
            };
            return _mediator.Send(updateUserRequest, cancellationToken);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeleteUserRequest deleteUserRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(deleteUserRequest, cancellationToken);
        }

        [Authorize]
        [HttpPatch]
        public Task<Result> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(changePasswordRequest, cancellationToken);
        }
    }
}
