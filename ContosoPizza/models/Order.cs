namespace ContosoPizza.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        
        public virtual List<Item> Items { get; set; }

    }
}