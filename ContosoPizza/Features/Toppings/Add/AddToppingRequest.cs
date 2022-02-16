using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Add
{
    public class AddToppingRequest : IRequest<ResultOf<int>>
    {
        public string Name { set; get; }
        public double Value { get; set; }
    }
}
