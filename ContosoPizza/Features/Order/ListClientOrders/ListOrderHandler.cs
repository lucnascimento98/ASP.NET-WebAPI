using ContosoPizza.DTOs;
using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Order.ListClientOrders
{
    public class ListOrderHandler : IRequestHandler<ListOrdersRequest, ResultOf<PageResult<OrderDTO>>>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ListOrderHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResultOf<PageResult<OrderDTO>>> Handle(ListOrdersRequest request, CancellationToken cancellationToken)
        {
            var orders = db.Orders.AsQueryable();
            var clientId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            if (!httpContextAccessor.HttpContext.User.Claims.Where(claim => claim.Type == "Claim").Select(d => Enum.Parse<Claims>(d.Value)).Contains(Claims.GetAllUsersOrders))
                orders = orders.Where(d => d.ClientId.ToString() == clientId );
            else 
                if (request.ClientId.HasValue)
                    orders = orders.Where(d => d.ClientId == request.ClientId.Value);

            var requestOrders = orders
                .Include(d => d.Client)
                .Include(d => d.Items)
                .ThenInclude(d => d.ItemToppings)
                .Include(d => d.Items)
                .ThenInclude(d => d.Pizza).ProjectToType<OrderDTO>();

            var total = await requestOrders.CountAsync(cancellationToken);

            var listOrders = await requestOrders.PaginateBy(request, o => o.ClientName).ToListAsync(cancellationToken);

            return new PageResult<OrderDTO>(request, total, listOrders);
            
        }
    }
}
