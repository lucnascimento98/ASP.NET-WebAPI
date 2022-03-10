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

            config.NewConfig<UpdatePizzaRequest, Pizza>()
                .Map(x => x.Name, x => x.Pizza.Name)
                .Map(x => x.Value, x => x.Pizza.Value)
                .Map(x => x.IsGlutenFree, x => x.Pizza.IsGlutenFree);
        }
    }
}
