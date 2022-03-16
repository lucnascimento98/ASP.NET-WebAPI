
using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Order.ListClientOrders
{
    public class ListOrdersRequest : PageRequest,  IRequest<ResultOf<PageResult<OrderDTO>>>
    {
        public int? ClientId { get; set; }
    }
}
