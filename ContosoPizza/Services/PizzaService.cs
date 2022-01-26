using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizza.Services
{
    public class PizzaService : IPizzaService
    {

        private ContosoPizzaContext Db { get; }
        public PizzaService(ContosoPizzaContext ctx)
        {
            Db = ctx;
        }

        public Task<List<Pizza>> GetAll(string search, int page, int quantity, bool? glutenFree, CancellationToken cancellationToken)
        {
            var pizzas = Db.Pizzas.Select(pizza => pizza);

            if (!String.IsNullOrWhiteSpace(search))
            {
                pizzas = pizzas.Where(pizza => pizza.Name.Contains(search));
            }

            if (glutenFree.HasValue)
            {
                pizzas = pizzas.Where(pizzas => pizzas.IsGlutenFree == glutenFree.Value);
            }

            int skipedElements = (page - 1) * quantity;

            pizzas = pizzas.OrderBy(p => p.Name).Skip(skipedElements).Take(quantity);

            return pizzas.ToListAsync(cancellationToken);
        }

        public Task<Pizza> Get(int id, CancellationToken cancellationToken) => Db.Pizzas.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public Task Add(Pizza pizza, CancellationToken cancellationToken)
        {
            Db.Pizzas.Add(pizza);
            return Db.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var pizza = await Get(id, cancellationToken);
            if (pizza is null)
                return;

            Db.Pizzas.Remove(pizza);
            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Update(int id, Pizza requestPizza, CancellationToken cancellationToken)
        {
            var pizza = await Get(id, cancellationToken);
            if (pizza is null)
                return false;

            pizza.Name = requestPizza.Name;
            pizza.IsGlutenFree = requestPizza.IsGlutenFree;
            pizza.Value = requestPizza.Value;

            await Db.SaveChangesAsync(cancellationToken);

            return true;
        }
       
    }
}