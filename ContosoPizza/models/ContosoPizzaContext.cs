using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Models
{
    public class ContosoPizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemTopping> ItemsToppings { get; set; }

        public ContosoPizzaContext()
        {

        }

        public ContosoPizzaContext(DbContextOptions options): base(options)
        {
            
        }
    }
}