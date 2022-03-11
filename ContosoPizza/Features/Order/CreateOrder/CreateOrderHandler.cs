using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System.Security.Claims;

namespace ContosoPizza.Features.Order.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, ResultOf<int>>
    {
        private readonly ContosoPizzaContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CreateOrderHandler(ContosoPizzaContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResultOf<int>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var requestPizzasIds = request.Items.Select(x => x.PizzaId).Distinct().ToArray();
            var requestToppingsIds = request.Items.SelectMany(x => x.ToppingsId).Distinct().ToArray();

            var pizzas = await db.Pizzas.Where(x => requestPizzasIds.Contains(x.Id)).ToListAsync(cancellationToken);
            var toppings = await db.Toppings.Where(x => requestToppingsIds.Contains(x.Id)).ToListAsync(cancellationToken);

            FieldErrors errors = new();

            foreach (var item in request.Items)
            {
                if (!pizzas.Any(x => x.Id == item.PizzaId))
                    errors.AddError("PizzaId: ", item.PizzaId.ToString() + "NotFound");

                foreach (var topping in item.ToppingsId)
                {
                    if (!toppings.Select(x => x.Id).Contains(topping))
                        errors.AddError("ToppingId: ", topping.ToString() + "NotFound");
                }
            }

            if (errors.InternalSource.Count > 0)
                return new BadRequestError() { FieldErrors = errors };

            var order = new Models.Order() { 
                Price = 0, 
                ClientId = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
            };

            db.Orders.Add(order);            

            foreach (var i in request.Items)
            {
                var item = new Item() { 
                    PizzaId = i.PizzaId, 
                    Order = order, 
                    Value = pizzas.FirstOrDefault(x => x.Id == i.PizzaId).Value 
                };

                db.Items.Add(item);

                foreach (var t in i.ToppingsId)
                {
                    var itemTopping = new ItemTopping()
                    {
                        Item = item,
                        ToppingId = t
                    };

                    db.ItemsToppings.Add(itemTopping);              

                    item.Value += toppings.FirstOrDefault(x => x.Id == t).Value;

                }
                order.Price += item.Value; 
            }

            await db.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
