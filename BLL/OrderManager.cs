using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrderManager : IOrderManager
    {
        private IOrderDB OrderDb { get; }
        private IOrderDetailDB OrderDetailDb { get; }
        public OrderManager(IOrderDB orderDb, IOrderDetailDB orderDetailDb)
        {
            OrderDb = orderDb;
            OrderDetailDb = orderDetailDb;
        }

        public Order CreateOrder(Order order, List<OrderDetail> orderDetails)
        {
            int createdOrderId = OrderDb.CreateOrder(order).OrderId;
            foreach(var orderDetail in orderDetails)
            {
                OrderDetailDb.InsertOrderDetail(createdOrderId, orderDetail);
            }
            

            return order;

        }

        public List<Order> GetOrderByUserId(int userId)
        {
            List<Order> result = OrderDb.GetOrders().FindAll(o => o.CustomerId == userId);
            return result;

        }
    }
}
