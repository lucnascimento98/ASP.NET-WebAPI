using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class ToppingService : IToppingService
    {
        private ContosoPizzaContext Db { get; }

        public ToppingService(ContosoPizzaContext ctx)
        {
            Db = ctx;
        }
        public async Task<List<Topping>> GetAll(string search, int page, int quantity, CancellationToken cancellationToken)
        {
            var topping = Db.Toppings.Select(topping => topping);

            if (!String.IsNullOrWhiteSpace(search))
            {
                topping = topping.Where(topping => topping.Name.Contains(search));
            }

            int skipedElements = (page - 1) * quantity;

            topping = topping.OrderBy(p => p.Name).Skip(skipedElements).Take(quantity);

            return await topping.ToListAsync(cancellationToken);
        }

        public Task<Topping> Get(int id, CancellationToken cancellationToken) => Db.Toppings.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public Task Add(Topping topping, CancellationToken cancellationToken)
        {
            Db.Toppings.Add(topping);
            return Db.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var topping = await Get(id, cancellationToken);
            if (topping is null)
                return;

            Db.Toppings.Remove(topping);
            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Update(int id, Topping requestTopping, CancellationToken cancellationToken)
        {
            var topping = await Get(id, cancellationToken);
            if (topping is null)
                return false;

            topping.Name = requestTopping.Name;
            topping.Value = requestTopping.Value;

            await Db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
