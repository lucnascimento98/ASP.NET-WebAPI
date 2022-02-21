using ContosoPizza.DTOs;
using ContosoPizza.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthentictionController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthentictionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public Task<ResultOf<AuthenticationResult>> Create([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(loginRequest, cancellationToken);
        }
    }
}
