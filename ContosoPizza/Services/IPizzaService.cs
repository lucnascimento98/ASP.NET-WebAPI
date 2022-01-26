using ContosoPizza.Models;

namespace ContosoPizza
{
    public interface IPizzaService
    {
        public Task<List<Pizza>> GetAll(string search, int page, int quantity, bool? glutenFree, CancellationToken cancellationToken);

        public Task<Pizza> Get(int id, CancellationToken cancellationToken);

        public Task Add(Pizza pizza, CancellationToken cancellationToken);

        public Task Delete(int id, CancellationToken cancellationToken);

        public Task<bool> Update(int id, Pizza requestPizza, CancellationToken cancellationToken);

    }
}
