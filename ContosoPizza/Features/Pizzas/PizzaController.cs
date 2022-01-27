using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using MediatR;
using ContosoPizza.Features.Pizzas;
using ContosoPizza.Features.Pizzas.Get;
using ContosoPizza.Features.Pizzas.GetAll;
using ContosoPizza.Features.Pizzas.Update;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PizzaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetAll([FromQuery]string search, [FromQuery] bool? glutenFree, CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int quantity = 10 )
        {
            GetAllPizzaRequest getAllPizzaRequest = new()
            {
                GlutenFree = glutenFree,
                Quantity = quantity,
                Page = page,
                Search = search
            };

            var Pizzas = await _mediator.Send(getAllPizzaRequest, cancellationToken);

            if (Pizzas.Count == 0)
            {
                return NoContent();
            }
            return Pizzas;
        }

        [HttpGet("id")]
        public async Task<ActionResult<Pizza>> Get(int id, CancellationToken cancellationToken)
        {
            GetPizzaRequest getPizzaRequest = new()
            {
                Id = id,
            };
            var pizza = await _mediator.Send(getPizzaRequest, cancellationToken);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPizzaRequest request, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(request, cancellationToken);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pizza pizza, CancellationToken cancellationToken)
        {
            UpdatePizzaRequest updatePizzaRequest = new()
            {
                Pizza = pizza,
                Id = id
            };

            if (!await _mediator.Send(updatePizzaRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            DeletePizzaRequest deletePizzaRequest = new()
            {
                Id = id
            };

            if (!await _mediator.Send(deletePizzaRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        } 
    }
}