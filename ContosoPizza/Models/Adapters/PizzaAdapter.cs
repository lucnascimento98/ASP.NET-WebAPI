using ContosoPizza.DTOs;
using ContosoPizza.Features.Pizzas;
using ContosoPizza.Features.Pizzas.Update;
using Mapster;

namespace ContosoPizza.Models.Adapters
{
    public class PizzaAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Pizza, PizzaDTO>()
                .TwoWays();

            config.NewConfig<AddPizzaRequest, Pizza>()
                .Ignore(x => x.Id)
                .Ignore(x => x.Items);

            config.NewConfig<UpdatePizzaRequest, Pizza>()
                .Ignore(x => x.Items)
                .Ignore(x => x.Id);
        }
    }
}
