using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContosoPizza.Features.Pizzas;
using ContosoPizza.Features.Pizzas.Get;
using ContosoPizza.Features.Pizzas.GetAll;
using ContosoPizza.Features.Pizzas.Update;
using ContosoPizza.DTOs;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

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
        public Task<ResultOf<PageResult<PizzaDTO>>> GetAll([FromQuery] GetAllPizzaRequest getAllPizzaRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllPizzaRequest, cancellationToken);
        }

        [HttpGet("{Id}")]
        public Task<ResultOf<PizzaDTO>> Get([FromRoute]GetPizzaRequest getPizzaRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getPizzaRequest, cancellationToken);
        }

        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] AddPizzaRequest addPizzaRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(addPizzaRequest, cancellationToken);

           
        }

        [HttpPut("{id}")]
        public Task<Result> Update(int id, [FromBody] UpdatePizzaRequestDTO pizzaDTO, CancellationToken cancellationToken)
        {
            UpdatePizzaRequest updatePizzaRequest = new()
            {
                Id = id,
                Pizza = pizzaDTO
            };
            return _mediator.Send(updatePizzaRequest, cancellationToken);
        }

        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeletePizzaRequest deletePizzaRequest, CancellationToken cancellationToken)
        {
            return  _mediator.Send(deletePizzaRequest, cancellationToken);
        } 
    }
}