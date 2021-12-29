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
            db = new ContosoPizzaContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Contoso").Options);
        }

        public static async Task<List<Pizza>> GetAll() => await db.Pizzas.ToListAsync();

        public static async Task<Pizza> Get(int id) => await db.Pizzas.FirstOrDefaultAsync(p => p.Id == id);

        public static async Task Add(Pizza pizza)
        {
            db.Pizzas.Add(pizza);
            await db.SaveChangesAsync();
        }

        public static async Task Delete(int id)
        {
            var pizza = await Get(id);
            if(pizza is null)
                return;

            db.Pizzas.Remove(pizza);
            await db.SaveChangesAsync();

        }

        public static async Task<bool> Update(int id, Pizza requestPizza)
        {
            var pizza = await Get(id);
            if(pizza is null)
                return false;
            
            pizza.Name = requestPizza.Name;
            pizza.IsGlutenFree = requestPizza.IsGlutenFree;
            
            await db.SaveChangesAsync();

            return true;
        }
    }
}