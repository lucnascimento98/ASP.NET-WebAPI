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
        public Task<List<Topping>> GetAll(string search, int page, int quantity)
        {
            var topping = Db.Toppings.Select(topping => topping);

            if (!String.IsNullOrWhiteSpace(search))
            {
                topping = topping.Where(topping => topping.Name.Contains(search));
            }

            int skipedElements = (page - 1) * quantity;

            topping = topping.OrderBy(p => p.Name).Skip(skipedElements).Take(quantity);

            return topping.ToListAsync();
        }

        public Task<Topping> Get(int id) => Db.Toppings.FirstOrDefaultAsync(p => p.Id == id);

        public Task Add(Topping topping)
        {
            Db.Toppings.Add(topping);
            return Db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var topping = await Get(id);
            if (topping is null)
                return;

            Db.Toppings.Remove(topping);
            await Db.SaveChangesAsync();
        }

        public async Task<bool> Update(int id, Topping requestTopping)
        {
            var topping = await Get(id);
            if (topping is null)
                return false;

            topping.Name = requestTopping.Name;
            topping.Value = requestTopping.Value;

            await Db.SaveChangesAsync();

            return true;
        }
    }
}
