namespace ContosoPizza.Models
{
    public class ItemTopping
    {
        public int Id { get; set; }
        
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int ToppingId { get; set; }
        public virtual Topping Topping { get; set; }    

        

    }

}