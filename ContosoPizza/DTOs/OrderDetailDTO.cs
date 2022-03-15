namespace ContosoPizza.DTOs
{
    public class OrderDetailDTO 
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public List<ItemDetailDTO> Items { get; set; }
    }
}
