using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Order.OrderDetail
{
    public class OrderDetailRequest : IRequest<ResultOf<OrderDetailDTO>>
    {
        public int OrderId { get; set; }
    }
}
