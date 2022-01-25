using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public interface IToppingService
    {
        Task Add(Topping topping);
        Task Delete(int id);
        Task<Topping> Get(int id);
        Task<List<Topping>> GetAll(string search, int page, int quantity);
        Task<bool> Update(int id, Topping requestTopping);
    }
}