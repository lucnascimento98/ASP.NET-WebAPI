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

            config.NewConfig<AddToppingRequest, Topping>()
                .Ignore(x => x.Id)
                .Ignore(x => x.ItemToppings);

            config.NewConfig<UpdateToppingRequest, Topping>()
                .Ignore(x => x.ItemToppings)
                .Ignore(x => x.Id);
        }
    }
}
