namespace ContosoPizza.DTOs
{
    public class UpdatePizzaRequestDTO
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
