using System.Collections.Generic;

namespace ContosoPizza.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsGlutenFree { get; set; }

        public virtual List<Item> Items { get; set; }
    }
    public class PizzaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}