namespace ContosoPizza.DTOs
{
    public class ItemDTO
    {

        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public List<ToppingLessDetailDTO> Toppings { get; set; }
     
    }
}
