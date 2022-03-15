using ContosoPizza.DTOs;
using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Order.OrderDetail
{
    public class OrderDetailHandler : IRequestHandler<OrderDetailRequest, ResultOf<OrderDetailDTO>>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderDetailHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResultOf<OrderDetailDTO>> Handle(OrderDetailRequest request, CancellationToken cancellationToken)
        {
            var order = await db.Orders
                .Include(x => x.Items).ThenInclude(y => y.Pizza)
                .Include(x => x.Items).ThenInclude(y => y.ItemToppings).ThenInclude(z => z.Topping)
                .Include(x => x.Client).FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);
            
            if (order == null)
                return new NotFoundError();
            
            var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            if (!httpContextAccessor.HttpContext.User.Claims.Where(claim => claim.Type == "Claim").Select(d => Enum.Parse<Claims>(d.Value)).Contains(Claims.GetAllUsersOrders)
                && order.ClientId.ToString() != userId)
                return new ForbiddenError();

            return new OrderDetailDTO()
            {
                Id = order.Id,
                Items = order.Items.Select(x => new ItemDetailDTO()
                {
                    Pizza = x.Pizza.Adapt<PizzaDTO>(),
                    Toppings = x.ItemToppings.Select(y => y.Topping.Adapt<ToppingDTO>()).ToList()
                }).ToList()
            };
        }
    }
}
