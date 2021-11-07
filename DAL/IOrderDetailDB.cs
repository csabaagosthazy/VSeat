using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IOrderDetailDB
    {
        List<OrderDetail> GetOrderItemsByOrderId(int orderId);
        OrderDetail GetOrderItemById(int orderDetailId);
        int AddOrderDetail(OrderDetail orderDetail);
        int UpdateOrderDetail(OrderDetail orderDetail);
        int DeleteOrderDetail(int orderDetailId);
    }
}
