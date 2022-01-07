namespace ContosoPizza.Models
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public double Value { get; set; }

        public virtual List<ItemTopping> ItemToppings { get; set; }

    }
}