using ContosoPizza.DTOs;
using Mapster;

namespace ContosoPizza.Models.Adapters
{
    public class RoleAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Role, RoleDTO>()
                .TwoWays();
        }
    }
}
