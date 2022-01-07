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
        public PizzaController()
        {
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll([FromQuery]string search, [FromQuery] bool? glutenFree, [FromQuery] int page = 1, [FromQuery] int quantity = 10 )
        {
            var Pizzas = PizzaService.GetAll(search, page, quantity, glutenFree);

            if (Pizzas.Count == 0)
            {
                return NoContent();
            }
            return Pizzas;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> Get(int id)
        {
            var pizza = await PizzaService.Get(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }








        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Pizza pizza)
        {
            await PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        










        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pizza pizza)
        {

            if (!await PizzaService.Update(id, pizza))
            {
                return NotFound();
            }

            return NoContent();
        }

        













        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var pizza = await PizzaService.Get(id);

            if (pizza is null)
            {
                return NotFound();
            }

            await PizzaService.Delete(id);

            return NoContent();
        }

        
    }
}