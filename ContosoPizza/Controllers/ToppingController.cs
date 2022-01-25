using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ToppingController : ControllerBase
    {
        private readonly IToppingService _toppingService;

        public ToppingController(IToppingService toppingService)
        {
            _toppingService = toppingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetAll([FromQuery] string search, [FromQuery] int page = 1, [FromQuery] int quantity = 10)
        {
            var topping = await _toppingService.GetAll(search, page, quantity);

            if (topping.Count == 0)
            {
                return NoContent();
            }
            return topping;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topping>> Get(int id)
        {
            var topping = await _toppingService.Get(id);

            if (topping == null)
            {
                return NotFound();
            }

            return topping;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Topping topping)
        {
            await _toppingService.Add(topping);
            return CreatedAtAction(nameof(Get), new { id = topping.Id }, topping);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Topping topping)
        {

            if (!await _toppingService.Update(id, topping))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var topping = await _toppingService.Get(id);

            if (topping is null)
            {
                return NotFound();
            }

            await _toppingService.Delete(id);

            return NoContent();
        }

    }
}
