using MediatR;

namespace ContosoPizza.Features.Toppings.Delete
{
    public class DeleteToppingRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
