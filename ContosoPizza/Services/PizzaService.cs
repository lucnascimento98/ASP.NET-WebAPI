using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizza.Services
{
    public static class PizzaService
    {

        private static ContosoPizzaContext db { get; }
        static PizzaService()
        {
            db = new ContosoPizzaContext(new DbContextOptionsBuilder().UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test").Options);
        }

        public static List<Pizza> GetAll(string search, int page, int quantity, bool? glutenFree)
        {
            //var Pizzas = from pizza in db.Pizzas select pizza;

            var pizzas = db.Pizzas.Select(pizza => pizza);

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

            return pizzas.ToList();
        }

        public static async Task<Pizza> Get(int id) => await db.Pizzas.FirstOrDefaultAsync(p => p.Id == id);

        public static async Task Add(Pizza pizza)
        {
            db.Pizzas.Add(pizza);
            await db.SaveChangesAsync();
        }

        public static async Task Delete(int id)
        {
            var pizza = await Get(id);
            if (pizza is null)
                return;

            db.Pizzas.Remove(pizza);
            await db.SaveChangesAsync();

        }

        public static async Task<bool> Update(int id, Pizza requestPizza)
        {
            var pizza = await Get(id);
            if (pizza is null)
                return false;

            pizza.Name = requestPizza.Name;
            pizza.IsGlutenFree = requestPizza.IsGlutenFree;

            await db.SaveChangesAsync();

            return true;
        }
    }
}