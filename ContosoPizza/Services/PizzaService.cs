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

        public Task<List<Pizza>> GetAll(string search, int page, int quantity, bool? glutenFree)
        {
            //var Pizzas = from pizza in db.Pizzas select pizza;

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

            return pizzas.ToListAsync();
        }

        public Task<Pizza> Get(int id) => Db.Pizzas.FirstOrDefaultAsync(p => p.Id == id);

        public Task Add(Pizza pizza)
        {
            Db.Pizzas.Add(pizza);
            return Db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var pizza = await Get(id);
            if (pizza is null)
                return;

            Db.Pizzas.Remove(pizza);
            await Db.SaveChangesAsync();
        }

        public async Task<bool> Update(int id, Pizza requestPizza)
        {
            var pizza = await Get(id);
            if (pizza is null)
                return false;

            pizza.Name = requestPizza.Name;
            pizza.IsGlutenFree = requestPizza.IsGlutenFree;
            pizza.Value = requestPizza.Value;

            await Db.SaveChangesAsync();

            return true;
        }
       
    }
}