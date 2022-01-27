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
        public async Task<ActionResult<List<Topping>>> GetAll([FromQuery] string search, CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int quantity = 10 )
        {
            GetAllToppingRequest getAllToppingRequest = new()
            {
                Quantity = quantity,
                Page = page,
                Search = search
            };

            var topping = await _mediator.Send(getAllToppingRequest, cancellationToken);

            if (topping.Count == 0)
            {
                return NoContent();
            }
            return topping;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topping>> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            GetToppingRequest getToppingRequest = new()
            {
                Id = id,
            };
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
        public async Task<IActionResult> Update(int id, [FromBody] Topping topping, CancellationToken cancellationToken)
        {

            UpdateToppingRequest updateToppingRequest = new()
            {
                Topping = topping,
                Id = id
            };

            if (!await _mediator.Send(updateToppingRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {

            DeleteToppingRequest deleteTopingRequest = new()
            {
                Id = id
            };

            if (!await _mediator.Send(deleteTopingRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
