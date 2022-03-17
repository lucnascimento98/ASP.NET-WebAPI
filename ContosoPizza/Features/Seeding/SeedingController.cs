using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Seeding
{
    [ApiController]
    [Route("[controller]")]
    public class SeedingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeedingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<Result> SeedDB(SeedDBRequest seedDBRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(seedDBRequest, cancellationToken);
        }
    }
}
