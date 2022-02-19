﻿using ContosoPizza.DTOs;
using ContosoPizza.Features.Users.Add;
using ContosoPizza.Features.Users.Delete;
using ContosoPizza.Features.Users.Get;
using ContosoPizza.Features.Users.GetAll;
using ContosoPizza.Features.Users.Update;
using MediatR;
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

        [HttpGet]
        public Task<ResultOf<PageResult<UserDTO>>> GetAll([FromQuery] GetAllUserRequest getAllUserRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllUserRequest, cancellationToken);
        }

        [HttpGet("{Id}")]
        public Task<ResultOf<UserDTO>> Get([FromRoute] GetUserRequest getUserRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getUserRequest, cancellationToken);
        }

        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] AddUserRequest request, CancellationToken cancellationToken)
        {
            return _mediator.Send(request, cancellationToken);

        }

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

        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeleteUserRequest deleteUserRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(deleteUserRequest, cancellationToken);
        }
    }
}
