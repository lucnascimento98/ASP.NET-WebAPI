using ContosoPizza.DTOs;
using ContosoPizza.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingHandler : IRequestHandler<GetAllToppingRequest, ResultOf<PageResult<ToppingDTO>>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<PageResult<ToppingDTO>>> Handle(GetAllToppingRequest request, CancellationToken cancellationToken)
        {
            var toppings = db.Toppings.AsQueryable();

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                toppings = toppings.Where(pizza => pizza.Name.Contains(request.Search));
            }

            var total = await toppings.CountAsync(cancellationToken);

            var list = await toppings.ProjectToType<ToppingDTO>().PaginateBy(request, p => p.Name).ToListAsync(cancellationToken);

            return new PageResult<ToppingDTO>(request, total, list);           
        }
    }
}
