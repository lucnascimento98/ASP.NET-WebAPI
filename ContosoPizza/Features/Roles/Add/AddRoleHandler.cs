using ContosoPizza.Models;
using MediatR;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Roles.Add
{
    public class AddRoleHandler : IRequestHandler<AddRoleRequest, ResultOf<int>>
    {
        private readonly ContosoPizzaContext db;

        public AddRoleHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }

        public async Task<ResultOf<int>> Handle(AddRoleRequest request, CancellationToken cancellationToken)
        {

            Role role = new()
            {
                Name = request.Name,
            };

            db.Roles.Add(role);

            await db.SaveChangesAsync(cancellationToken);

            return role.Id;
        }
    }
}
