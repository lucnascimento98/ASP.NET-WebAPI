using ContosoPizza.DTOs;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Order.CreateOrder
{
    public class CreateOrderRequest : IRequest<ResultOf<int>>
    {
        public List<CreateItemDTO> Items { get; set; }
    }
}
