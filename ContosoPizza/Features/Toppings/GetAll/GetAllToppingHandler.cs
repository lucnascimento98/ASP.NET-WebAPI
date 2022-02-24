﻿using ContosoPizza.DTOs;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Paginator.Core;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Toppings.GetAll
{
    public class GetAllToppingHandler : IRequestHandler<GetAllToppingRequest, ResultOf<PageResult<ToppingDTO>>>
    {
        private readonly ContosoPizzaContext db;

        public GetAllToppingHandler(ContosoPizzaContext db)
        {
            this.db = db;
        }
        public async Task<ResultOf<PageResult<ToppingDTO>>> Handle(GetAllToppingRequest request, CancellationToken cancellationToken)
        {
            var toppings = db.Toppings.AsQueryable();

            if (!String.IsNullOrWhiteSpace(request.Search))
            {
                toppings = toppings.Where(pizza => pizza.Name.Contains(request.Search));
            }

            var total = await toppings.CountAsync(cancellationToken);

            List<ToppingDTO> list = await toppings.Select(t => new ToppingDTO()
            {
                Id = t.Id,
                Name = t.Name,
                Value = t.Value
            }).PaginateBy(request, p => p.Name).ToListAsync(cancellationToken);


            return new PageResult<ToppingDTO>(request, total, list);

            
            
        }
    }
}
