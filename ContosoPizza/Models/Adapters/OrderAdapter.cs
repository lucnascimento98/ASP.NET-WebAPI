using ContosoPizza.DTOs;
using Mapster;

namespace ContosoPizza.Models.Adapters
{
    public class OrderAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDTO>()
                .Map(x => x.OrderId, x => x.Id);
        }
    }
}
