using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetAll([FromQuery]string search, [FromQuery] bool? glutenFree, CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int quantity = 10 )
        {
            var Pizzas = await _pizzaService.GetAll(search, page, quantity, glutenFree, cancellationToken);

            if (Pizzas.Count == 0)
            {
                return NoContent();
            }
            return Pizzas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> Get(int id, CancellationToken cancellationToken)
        {
            var pizza = await _pizzaService.Get(id, cancellationToken);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Pizza pizza, CancellationToken cancellationToken)
        {
            await _pizzaService.Add(pizza, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pizza pizza, CancellationToken cancellationToken)
        {

            if (!await _pizzaService.Update(id, pizza, cancellationToken))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {

            var pizza = await _pizzaService.Get(id, cancellationToken);

            if (pizza is null)
            {
                return NotFound();
            }

            await _pizzaService.Delete(id, cancellationToken);

            return NoContent();
        } 
    }
}