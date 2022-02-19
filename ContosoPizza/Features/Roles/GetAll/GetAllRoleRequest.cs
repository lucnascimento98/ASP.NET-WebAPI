using ContosoPizza.DTOs;
using MediatR;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.GetAll
{
    public class GetAllRoleRequest : PageRequest, IRequest<ResultOf<PageResult<RoleDTO>>>
    {
        public string Search { get; set; } 
    }
}
