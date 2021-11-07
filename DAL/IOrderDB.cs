using DTO;

namespace DAL
{
    public interface IOrderDB
    {
        Order CreateOrder(Order order);
        List<Order> GetOrders();
        Order GetOrderById(int id);
        int AddOrder(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(int id);
    }
}