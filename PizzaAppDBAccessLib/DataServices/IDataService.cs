using PizzaAppDBAccessLib.Models;

namespace PizzaAppDBAccessLib.DataServices
{
    public interface IDataService
    {
        List<Pizza> GetAllPizzas();

        Pizza? GetPizzaById(int id);

        void OrderPizza(FullOrder fullOrder);
        List<FullOrder> GetAllOrdersToDeliver();

        void DeliveryDone(int orderId);
    }
}
