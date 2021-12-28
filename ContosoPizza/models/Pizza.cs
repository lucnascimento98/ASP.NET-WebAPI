using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Models
{
    public class ContosoPizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public string DbPath { get; }

        public ContosoPizzaContext()
        {
        }

        public ContosoPizzaContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(databaseName: "ContosoPizza");
        }
    }
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public bool IsGlutenFree { get; set; }
    }
}