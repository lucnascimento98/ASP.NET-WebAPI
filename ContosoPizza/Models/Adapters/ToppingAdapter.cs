using ContosoPizza.DTOs;
using ContosoPizza.Features.Toppings.Add;
using ContosoPizza.Features.Toppings.Update;
using Mapster;

namespace ContosoPizza.Models.Adapters
{
    public class ToppingAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Topping, ToppingDTO>()
                .TwoWays();

            config.NewConfig<UpdateToppingRequest, Topping>()
                .Map(x => x.Name, x => x.Topping.Name)
                .Map(x => x.Value, x => x.Topping.Value);
        }
    }
}
