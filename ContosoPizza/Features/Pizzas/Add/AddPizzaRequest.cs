using MediatR;
using Nudes.Retornator.Core;

namespace ContosoPizza.Features.Pizzas;

/// <summary>
/// Classe de requisição, precisa implantar IRequest<T> onde T é o resultado, nesse caso iremos retornar o Id da pizza criada
/// </summary>
public class AddPizzaRequest : IRequest<ResultOf<int>>
{
    public string Name { get; set; }
    public double Value { get; set; }
    public bool IsGlutenFree { get; set; }
}
