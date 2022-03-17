using ContosoPizza.DTOs;
using ContosoPizza.Features.Users.Add;
using ContosoPizza.Features.Users.Update;
using Mapster;

namespace ContosoPizza.Models.Adapters
{
    public class UserAdapter : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDTO>()
                .Map(x => x.Role, x => x.Role.Name)
                .TwoWays();

            config.NewConfig<AddUserRequest, User>()
                .Ignore(x => x.PasswordHash);

            config.NewConfig<UpdateUserRequest, User>()
                .Map(x => x.Name, x => x.User.Name)
                .Map(x => x.Email, x => x.User.Email);
        }
    }
}
