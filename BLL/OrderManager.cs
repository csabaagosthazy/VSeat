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
            Order createdOrder = OrderDb.CreateOrder(order);
            long createdOrderId = createdOrder.OrderId;
            foreach(var orderDetail in orderDetails)
            {
                OrderDetailDb.InsertOrderDetail((int)createdOrderId, orderDetail);
            }
            

            return createdOrder;

        }

        public List<Order> GetOrderByUserId(string userId)
        {
            List<Order> allOrders = OrderDb.GetOrders();
            List<Order> result = allOrders.FindAll(o => o.CustomerId.Equals(userId));
            return result;

        }

        public List<Order> GetOrderByCourierId(string userId)
        {      
            return OrderDb.GetOrderByCourierId(userId);
        }

        public Order GetOrderById(long orderId)
        {
            return OrderDb.GetOrderById(orderId);
        }

        public int DeliverOrderById(int orderId)
        {
            return OrderDb.DeliverOrderById(orderId);
        }

        public int CancelOrder(Order order)
        {
            order.IsCancel = true;
            return OrderDb.UpdateOrder(order);
        }
    }
}
