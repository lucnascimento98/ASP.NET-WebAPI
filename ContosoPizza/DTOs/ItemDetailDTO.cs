namespace ContosoPizza.DTOs
{
    public class ItemDetailDTO
    {
        public PizzaDTO Pizza { get; set; }
        public List<ToppingDTO> Toppings { get; set; }

    }
}
