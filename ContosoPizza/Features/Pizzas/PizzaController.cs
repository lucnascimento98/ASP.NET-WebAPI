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
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
        [HttpGet]
        public Task<ResultOf<PageResult<PizzaDTO>>> GetAll([FromQuery] GetAllPizzaRequest getAllPizzaRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getAllPizzaRequest, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("{Id}")]
        public Task<ResultOf<PizzaDTO>> Get([FromRoute]GetPizzaRequest getPizzaRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(getPizzaRequest, cancellationToken);
        }

        [Authorize(Policy = "AddPizza")]
        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] AddPizzaRequest addPizzaRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(addPizzaRequest, cancellationToken);

           
        }

        [Authorize(Policy = "EditPizza")]
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

        [Authorize(Policy = "DeletePizza")]
        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeletePizzaRequest deletePizzaRequest, CancellationToken cancellationToken)
        {
            return  _mediator.Send(deletePizzaRequest, cancellationToken);
        } 
    }
}