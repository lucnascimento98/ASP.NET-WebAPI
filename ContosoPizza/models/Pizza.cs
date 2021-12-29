using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Models
{
    public class ContosoPizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public ContosoPizzaContext(DbContextOptions options): base(options)
        {
            
        }
    }



    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public bool IsGlutenFree { get; set; }
    }
}