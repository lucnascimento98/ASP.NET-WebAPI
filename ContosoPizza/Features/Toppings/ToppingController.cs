using ContosoPizza.DTOs;
using ContosoPizza.Features.Toppings.Add;
using ContosoPizza.Features.Toppings.Delete;
using ContosoPizza.Features.Toppings.Get;
using ContosoPizza.Features.Toppings.GetAll;
using ContosoPizza.Features.Toppings.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ToppingController : ControllerBase
    {
        private readonly IMediator _mediator;


        public ToppingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<ResultOf<PageResult<ToppingDTO>>> GetAll([FromQuery] GetAllToppingRequest getAllToppingRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllToppingRequest, cancellationToken);
        }

        [HttpGet("{Id}")]
        public Task<ResultOf<ToppingDTO>> Get([FromRoute] GetToppingRequest getToppingRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getToppingRequest, cancellationToken);
        }

        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] AddToppingRequest addToppingRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(addToppingRequest, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<Result> Update(int id, [FromBody] UpdateToppingRequestDTO toppingDTO, CancellationToken cancellationToken)
        {

            UpdateToppingRequest updateToppingRequest = new()
            {
                Id = id,
                Topping = toppingDTO
            };

            return _mediator.Send(updateToppingRequest, cancellationToken);
        }

        [HttpDelete("{Id}")]
        public  Task<Result> Delete([FromRoute]DeleteToppingRequest deleteTopingRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(deleteTopingRequest, cancellationToken);
        }

    }
}
