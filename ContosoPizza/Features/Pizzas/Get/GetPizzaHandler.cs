﻿using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Features.Pizzas.Get
{
    public class GetPizzaHandler : IRequestHandler<GetPizzaRequest, Pizza>
    {
        private readonly ContosoPizzaContext db;

        public GetPizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<Pizza> Handle(GetPizzaRequest request, CancellationToken cancellationToken)
        {
           return await db.Pizzas.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        }
    }
}
