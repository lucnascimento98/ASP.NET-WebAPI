using ContosoPizza.Models;

using MediatR;

namespace ContosoPizza.Features.Pizzas;

/// <summary>
/// Classe de implementação da regra de negócio, precisa implementar IRequestHandler<T1,T2> onde T1 é a request e T2 é o retorno da request
/// </summary>
class AddPizzaHandler : IRequestHandler<AddPizzaRequest, int>
{

    private readonly ContosoPizzaContext db;

    public AddPizzaHandler(ContosoPizzaContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Metodo que será chamado pelo MediatR com a request, é o método que de facto implementa a regra de negócio
    /// </summary>
    public async Task<int> Handle(AddPizzaRequest request, CancellationToken cancellationToken)
    {
        Pizza pizza = new()
        {
            Name = request.Name,
            Value = request.Value,
            IsGlutenFree = request.IsGlutenFree
        };

        db.Pizzas.Add(pizza);

        await db.SaveChangesAsync(cancellationToken);

        return pizza.Id;
    }
}