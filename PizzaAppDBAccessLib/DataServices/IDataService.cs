using PizzaAppDBAccessLib.Models;

namespace PizzaAppDBAccessLib.Data
{
    public interface IDataService
    {
        List<Pizza> GetAllPizzas();

        Pizza? GetPizzaById(int id);

        void OrderPizza(FullOrder fullOrder);
    }
}
