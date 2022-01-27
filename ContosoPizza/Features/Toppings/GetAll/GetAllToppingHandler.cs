using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingHandler : IRequestHandler<GetAllToppingRequest, List<Topping>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<List<Topping>> Handle(GetAllToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = db.Toppings.Select(topping => topping);

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                topping = topping.Where(pizza => pizza.Name.Contains(request.Search));
            }

            int skipedElements = (request.Page - 1) * request.Quantity;

            topping = topping.OrderBy(p => p.Name).Skip(skipedElements).Take(request.Quantity);

            return await topping.ToListAsync(cancellationToken);
        }
    }
}
