using ContosoPizza.Models;

namespace ContosoPizza
{
    public interface IPizzaService
    {
        public Task<List<Pizza>> GetAll(string search, int page, int quantity, bool? glutenFree);

        public Task<Pizza> Get(int id);

        public Task Add(Pizza pizza);

        public Task Delete(int id);

        public Task<bool> Update(int id, Pizza requestPizza);

    }
}
