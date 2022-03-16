namespace ContosoPizza.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        
        public virtual List<Item> Items { get; set; }

        public int ClientId { get; set; }
        public virtual User Client { get; set; }

    }
}