using MediatR;

namespace ContosoPizza.Features.Pizzas
{
    public class DeletePizzaRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
