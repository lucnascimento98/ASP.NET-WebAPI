using ContosoPizza.DTOs;
using ContosoPizza.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public Task<ResultOf<AuthenticationResult>> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(loginRequest, cancellationToken);
        }
    }
}
