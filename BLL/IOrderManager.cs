using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    public interface IOrderManager
    {
        Order CreateOrder(Order order, List<OrderDetail> orderDetails);
        List<Order> GetOrderByUserId(int userId);

    }
}
