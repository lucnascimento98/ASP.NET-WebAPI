﻿using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;

namespace ContosoPizza.Features.Pizzas.GetAll
{
    public class GetAllPizzaHandler : IRequestHandler<GetAllPizzaRequest, List<PizzaDTO>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllPizzaHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<List<PizzaDTO>> Handle(GetAllPizzaRequest request, CancellationToken cancellationToken)
        {
            var pizzas = db.Pizzas.Select(pizza => pizza);

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                pizzas = pizzas.Where(pizza => pizza.Name.Contains(request.Search));
            }

            if (request.GlutenFree.HasValue)
            {
                pizzas = pizzas.Where(pizzas => pizzas.IsGlutenFree == request.GlutenFree.Value);
            }

            return await pizzas.Select(p => new PizzaDTO()
            {
                Id = p.Id,
                Name = p.Name,
                IsGlutenFree = p.IsGlutenFree
            }).PaginateBy(new PageRequest()
            {
                Field = "name",
                SortDirection = SortDirection.Ascending,
                Page = request.Page,
                PageSize = request.Quantity
            }).ToListAsync(cancellationToken);

        }
    }
}
