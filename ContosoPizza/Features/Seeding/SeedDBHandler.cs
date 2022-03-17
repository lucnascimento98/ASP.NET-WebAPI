using ContosoPizza.DTOs;
using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Seeding
{
    public class SeedDBHandler : IRequestHandler<SeedDBRequest, Result>
    {
        private readonly ContosoPizzaContext db;

        public SeedDBHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Result> Handle(SeedDBRequest request, CancellationToken cancellationToken)
        {
            await CleanDB(cancellationToken);
            SeedPizzas();
            SeedToppings();
            SeedRoleAndClaimsAndUsers();
            await db.SaveChangesAsync(cancellationToken);


            return Result.Success;
        }

        private async Task CleanDB(CancellationToken cancellationToken)
        {
            var itemtoppings = await db.ItemsToppings.ToListAsync(cancellationToken);
            foreach (var item in itemtoppings)
            {
                db.ItemsToppings.Remove(item);
            }

            var items = await db.Items.ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                db.Items.Remove(item);
            }

            var orders = await db.Orders.ToListAsync(cancellationToken);
            foreach (var item in orders)
            {
                db.Orders.Remove(item);
            }

            var users = await db.Users.ToListAsync(cancellationToken);
            foreach (var item in users)
            {
                db.Users.Remove(item);
            }

            var roleClaims = await db.RoleClaims.ToListAsync(cancellationToken);
            foreach (var role in roleClaims)
            {
                db.RoleClaims.Remove(role);
            }

            var roles = await db.Roles.ToListAsync(cancellationToken);
            foreach (var item in roles)
            {
                db.Roles.Remove(item);
            }

            var toppings = await db.Toppings.ToListAsync(cancellationToken);
            foreach (var item in toppings)
            {
                db.Toppings.Remove(item);
            }

            var pizzas = await db.Pizzas.ToListAsync(cancellationToken);
            foreach (var item in pizzas)
            {
                db.Pizzas.Remove(item);
            }

            await db.SaveChangesAsync(cancellationToken);
        }

        private void SeedRoleAndClaimsAndUsers()
        {
            Role cliente = new() { Name = "Cliente" };
            db.Roles.Add(cliente);
            
            List<Claims> clientClaims = new() { Claims.OrderPizza };
            foreach (var claim in clientClaims)
            {
                db.RoleClaims.Add(new Models.RoleClaim()
                {
                    Role = cliente,
                    Claim = claim
                });
            }

            Role funcionario = new() { Name = "Funcionario" };
            db.Roles.Add(funcionario);

            List<Claims> funcionarioClaims = new() {
                Claims.AddPizza, Claims.EditPizza, Claims.DeletePizza,
                Claims.AddTopping, Claims.EditTopping, Claims.DeleteTopping,
                Claims.AddUserAdmin, Claims.EditAllUser, Claims.DeleteAllUser, Claims.GetUser, Claims.GetAllUser,
                Claims.AddRole, Claims.DeleteRole, Claims.EditRole, Claims.GetRole, Claims.GetAllRole,
                Claims.AddClaimToRole, Claims.RemoveClaimFromRole, Claims.ListRoleClaims,
                Claims.GetAllUsersOrders
            };

            foreach(var claim in funcionarioClaims)
            {
                db.RoleClaims.Add(new Models.RoleClaim()
                {
                    Role = funcionario,
                    Claim = claim
                });
            }

            List<string> names = new() { "Lucas", "Patrick", "Vitor", "Anderson", "Erika" };
            List<string> emails = new() { "lucas@luc.com", "patrick@patrick.com", "vitinho@vitor.com", "anderson@and.com", "erika@erika.com" };
            string password = "123abc";
    
            for (int i = 0; i < names.Count; i++)
            {
                db.Users.Add(new Models.User()
                {
                    Name = names[i],
                    Email = emails[i],
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = (i%2==0)? funcionario : cliente

                });
            }
        }

        private void SeedToppings()
        {
            List<string> toppings = new() { "Cebola", "Alho", "Bacon", "Milho", "Ovo", "Queijo", "Tomate", "Catupiry" };
            List<double> values = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach( var to in toppings)
            {
                db.Toppings.Add(new Topping()
                {
                    Name = to,
                    Value = values[toppings.IndexOf(to)]
                });
            }
        }

        private void SeedPizzas()
        {
            List<string> pizzas = new() { "Mussarela", "Calabresa", "Marguerita", "Portuguesa", "Atum", "Champignon", "4 Queijos", "Napolitana", "Lombo", "A Moda da Casa" };
            List<double> values = new() { 5, 10, 20, 30, 15, 25, 35, 40, 45, 50 };

            foreach (var pi in pizzas)
            {
                db.Pizzas.Add(new Pizza()
                {
                    Name = pi,
                    Value = values[pizzas.IndexOf(pi)],
                    IsGlutenFree = values[pizzas.IndexOf(pi)] % 2 == 0
                });
            }
        }
    }
}
