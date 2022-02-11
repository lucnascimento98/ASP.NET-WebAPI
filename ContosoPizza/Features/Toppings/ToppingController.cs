using ContosoPizza.DTOs;
using ContosoPizza.Features.Toppings.Add;
using ContosoPizza.Features.Toppings.Delete;
using ContosoPizza.Features.Toppings.Get;
using ContosoPizza.Features.Toppings.GetAll;
using ContosoPizza.Features.Toppings.Update;
using ContosoPizza.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<ToppingDTO>>> GetAll([FromQuery] GetAllToppingRequest getAllToppingRequest, CancellationToken cancellationToken)
        {
            var topping = await _mediator.Send(getAllToppingRequest, cancellationToken);

            if (topping.Count == 0)
            {
                return NoContent();
            }
            return topping;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ToppingDTO>> Get([FromRoute] GetToppingRequest getToppingRequest, CancellationToken cancellationToken)
        {
            var topping = await _mediator.Send(getToppingRequest, cancellationToken);

            if (topping == null)
            {
                return NotFound();
            }

            return topping;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddToppingRequest request, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateToppingRequestDTO toppingDTO, CancellationToken cancellationToken)
        {

            UpdateToppingRequest updateToppingRequest = new()
            {
                Id = id,
                Topping = toppingDTO
            };

            if (!await _mediator.Send(updateToppingRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteToppingRequest deleteTopingRequest, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(deleteTopingRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
