using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingHandler : IRequestHandler<GetAllToppingRequest, List<ToppingDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<List<ToppingDTO>> Handle(GetAllToppingRequest request, CancellationToken cancellationToken)
        {
            var toppings = db.Toppings.Select(topping => topping);

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                toppings = toppings.Where(pizza => pizza.Name.Contains(request.Search));
            }

            return await toppings.Select(t => new ToppingDTO()
            {
                Id = t.Id,
                Name = t.Name,
                Value = t.Value
            }).PaginateBy(new PageRequest()
            {
                Field = "name",
                Page = request.Page,
                PageSize = request.Quantity,
                SortDirection = SortDirection.Ascending
            }).ToListAsync(cancellationToken);

            
        }
    }
}
