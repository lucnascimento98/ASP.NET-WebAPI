using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaHandler : IRequestHandler<GetPizzaRequest, PizzaDTO>
    {
        private readonly ContosoPizzaContext db;

        public GetPizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<PizzaDTO> Handle(GetPizzaRequest request, CancellationToken cancellationToken)
        {
            var pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (pizza == null) 
                return null;

            return new PizzaDTO()
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Value = pizza.Value,
                IsGlutenFree = pizza.IsGlutenFree
            };
        }
    }
}
