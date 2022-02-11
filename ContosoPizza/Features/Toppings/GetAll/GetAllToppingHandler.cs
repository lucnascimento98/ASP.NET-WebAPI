using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

            int skipedElements = (request.Page - 1) * request.Quantity;

            toppings = toppings.OrderBy(p => p.Name).Skip(skipedElements).Take(request.Quantity);
            
            List<ToppingDTO> toppingsDTO = await toppings.Select(t => new ToppingDTO()
            {
                Id = t.Id,
                Name = t.Name,
                Value = t.Value
            }).ToListAsync(cancellationToken);

            return toppingsDTO;
        }
    }
}
