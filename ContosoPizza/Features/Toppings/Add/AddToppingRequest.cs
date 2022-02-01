using MediatR;

namespace ContosoPizza.Features.Toppings.Add
{
    public class AddToppingRequest : IRequest<int>
    {
        public string Name { set; get; }
        public double Value { get; set; }
    }
}
