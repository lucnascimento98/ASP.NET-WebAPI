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
        public async Task<ActionResult<List<Pizza>>> GetAll() => await PizzaService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> Get(int id){
            var pizza = await PizzaService.Get(id);

            if (pizza == null){
                return NotFound();
            }

            return pizza;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pizza pizza){
            await PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id}, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update( int id, Pizza pizza){
            
            if (! await PizzaService.Update(id, pizza)){
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){

            var pizza = await PizzaService.Get(id);

            if (pizza is null){
                return NotFound();
            }

            await PizzaService.Delete(id);

            return NoContent();
        }
    }
}