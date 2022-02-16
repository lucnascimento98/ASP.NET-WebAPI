﻿using ContosoPizza.Features.Toppings;
using ContosoPizza.Models;
using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.Add
{
    public class AddToppingHandler : IRequestHandler<AddToppingRequest, ResultOf<int>>
    {
        private readonly ContosoPizzaContext db;

        public AddToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<int>> Handle(AddToppingRequest request, CancellationToken cancellationToken)
        {
            Topping topping = new()
            {
                Name = request.Name,
                Value = request.Value,
            };

            db.Toppings.Add(topping);

            await db.SaveChangesAsync(cancellationToken);

            return topping.Id;
        }
    }
}
