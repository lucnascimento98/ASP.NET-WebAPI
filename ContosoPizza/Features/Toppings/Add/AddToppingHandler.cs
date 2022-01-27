using ContosoPizza.Features.Toppings;
using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.Add
{
    public class AddToppingHandler : IRequestHandler<AddToppingRequest, int>
    {
        private readonly ContosoPizzaContext db;

        public AddToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<int> Handle(AddToppingRequest request, CancellationToken cancellationToken)
        {
            Topping topping = new()
            {
                Name = request.Name,
                Value = request.Value,
            };

            db.Toppings.Add(topping);

            await db.SaveChangesAsync(cancellationToken);

            return topping.Id;
        }
    }
}
