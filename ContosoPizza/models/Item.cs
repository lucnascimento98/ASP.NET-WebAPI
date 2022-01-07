namespace ContosoPizza.Models
{
    public class Item
    {
        public int Id { get; set; }
        public double Value { get; set; }

        public int PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; } //inverse navigation property

        public virtual List<ItemTopping> ItemToppings { get; set; }
        
    }

}