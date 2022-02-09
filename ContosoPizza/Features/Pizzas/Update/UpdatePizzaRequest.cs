using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Pizzas.Update
{
    public class UpdatePizzaRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsGlutenFree { get; set; }
    }

    public class UpdatePizzaRequestDTO
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
