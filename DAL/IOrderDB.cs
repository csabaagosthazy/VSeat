using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IOrderDB
    {
        Order CreateOrder(Order order);
        List<Order> GetOrders();
        Order GetOrderById(long id);
        int AddOrder(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(int id);
    }
}