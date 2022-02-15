using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingHandler : IRequestHandler<GetAllToppingRequest, List<Topping>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public Task<List<Topping>> Handle(GetAllToppingRequest request, CancellationToken cancellationToken)
        {
            var topping = db.Toppings.Select(topping => topping);

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                topping = topping.Where(pizza => pizza.Name.Contains(request.Search));
            }

            return topping.PaginateBy(new PageRequest()
            {
                Field = "name",
                Page = request.Page,
                PageSize = request.Quantity,
                SortDirection = SortDirection.Ascending
            }).ToListAsync(cancellationToken);
        }
    }
}
