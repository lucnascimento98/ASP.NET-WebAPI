using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizza.Services
{
    public static class PizzaService
    {
        static List<Pizza> Pizzas { get; }

        private static ContosoPizzaContext db { get; }
        static int nextId = 3;
        static PizzaService()
        {
            db = new  ContosoPizzaContext();

            Pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
                new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
            };
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