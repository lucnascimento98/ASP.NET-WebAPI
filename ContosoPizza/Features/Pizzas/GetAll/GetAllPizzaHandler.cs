using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaHandler : IRequestHandler<GetAllPizzaRequest, ResultOf<PageResult<PizzaDTO>>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllPizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<PageResult<PizzaDTO>>> Handle(GetAllPizzaRequest request, CancellationToken cancellationToken)
        {
            var pizzas = db.Pizzas.Select(pizza => pizza);

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                pizzas = pizzas.Where(pizza => pizza.Name.Contains(request.Search));
            }

            if (request.GlutenFree.HasValue)
            {
                pizzas = pizzas.Where(pizzas => pizzas.IsGlutenFree == request.GlutenFree.Value);
            }

            var total = await pizzas.CountAsync(cancellationToken);

            List<PizzaDTO> list = await pizzas.Select(p => new PizzaDTO()
            {
                Id = p.Id,
                Name = p.Name,
                IsGlutenFree = p.IsGlutenFree
            }).PaginateBy(request, p => p.Name).ToListAsync(cancellationToken);


            return new PageResult<PizzaDTO>(request, total, list);
        }
    }
}
