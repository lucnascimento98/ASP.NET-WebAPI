using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Add
{
    public class AddRoleRequest : IRequest<ResultOf<int>>
    {
        public string Name { get; set; }
    }
}
