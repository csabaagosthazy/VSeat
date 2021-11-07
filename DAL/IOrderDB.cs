using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IOrderDB
    {
        List<Order> GetOrders();
        Order GetOrderById(int id);
        int AddOrder(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(int id);

    }
}
