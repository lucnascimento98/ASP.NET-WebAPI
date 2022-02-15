using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using MediatR;
using ContosoPizza.Features.Pizzas;
using ContosoPizza.Features.Pizzas.Get;
using ContosoPizza.Features.Pizzas.GetAll;
using ContosoPizza.Features.Pizzas.Update;
using ContosoPizza.DTOs;

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
        public async Task<ActionResult<List<PizzaDTO>>> GetAll([FromQuery] GetAllPizzaRequest getAllPizzaRequest, CancellationToken cancellationToken)
        {
            var pizzas = await _mediator.Send(getAllPizzaRequest, cancellationToken);

            if (pizzas.Count == 0)
            {
                return NoContent();
            }
            return pizzas;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PizzaDTO>> Get([FromRoute]GetPizzaRequest getPizzaRequest, CancellationToken cancellationToken)
        {
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePizzaRequestDTO pizzaDTO, CancellationToken cancellationToken)
        {
            UpdatePizzaRequest updatePizzaRequest = new()
            {
                Id = id,
                Pizza = pizzaDTO
            };

            if (!await _mediator.Send(updatePizzaRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeletePizzaRequest deletePizzaRequest, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(deletePizzaRequest, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        } 
    }
}