using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaHandler : IRequestHandler<GetAllPizzaRequest, List<PizzaDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllPizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<List<PizzaDTO>> Handle(GetAllPizzaRequest request, CancellationToken cancellationToken)
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

            int skipedElements = (request.Page - 1) * request.Quantity;

            pizzas = pizzas.OrderBy(p => p.Name).Skip(skipedElements).Take(request.Quantity);


            List<PizzaDTO> pizzasDTO = await pizzas.Select(p => new PizzaDTO()
            {
                Id = p.Id,
                Name = p.Name,
                IsGlutenFree = p.IsGlutenFree
            }).ToListAsync(cancellationToken);


            return pizzasDTO;
        }
    }
}
