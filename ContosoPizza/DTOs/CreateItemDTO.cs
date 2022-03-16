namespace ContosoPizza.DTOs
{
    public class CreateItemDTO
    {
        public int PizzaId { get; set; }
        public List<int> ToppingsId { get; set; }
    }
}
