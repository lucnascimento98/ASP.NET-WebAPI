namespace ContosoPizza.DTOs
{
    public class OrderDetailDTO 
    {
        public int Id { get; set; }
        public List<ItemDetailDTO> Items { get; set; }
    }
}
