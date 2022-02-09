using ContosoPizza.Models;
using MediatR;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public double Value { get; set; }
    }
    public class UpdateToppingRequestDTO
    {
        public string Name { set; get; }
        public double Value { get; set; }
    }
}
