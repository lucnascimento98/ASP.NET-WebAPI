using ContosoPizza.DTOs;
using ContosoPizza.Features.Order.CreateOrder;
using ContosoPizza.Features.Order.ListClientOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Order
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public Task<ResultOf<PageResult<OrderDTO>>> ListClientOrders([FromQuery]ListOrdersRequest listOrdersRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(listOrdersRequest, cancellationToken);
        }

        [Authorize(Policy = "OrderPizza")]
        [HttpPost]
        public Task<ResultOf<int>> OrderPizza(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken)
        {
            return _mediator.Send(createOrderRequest, cancellationToken);
        }

    }
}
