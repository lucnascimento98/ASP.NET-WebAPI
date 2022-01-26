using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public interface IToppingService
    {
        Task Add(Topping topping, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<Topping> Get(int id, CancellationToken cancellationToken);
        Task<List<Topping>> GetAll(string search, int page, int quantity, CancellationToken cancellationToken);
        Task<bool> Update(int id, Topping requestTopping, CancellationToken cancellationToken);
    }
}