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

        public List<Order> GetOrderByUserId(int userId)
        {
            List<Order> result = new List<Order>();
            List<Order> orders = OrderDb.GetOrders();
            if(orders != null)
            {
                result = orders.FindAll(o => o.CustomerId == userId);
            }
            return result;

        }

        public List<Order> GetOrdersByCourierId(int courierId)
        {
            List<Order> result = new List<Order>();
            List<Order> orders = OrderDb.GetOrders();
            if (orders != null)
            {
                result = orders.FindAll(order => order.CourierId == courierId);

            }
            return result;
        }

        public Order GetOrderById(long orderId)
        {
            return OrderDb.GetOrderById(orderId);
        }

        public int DeliverOrderById(int orderId)
        {
            Order order = OrderDb.GetOrderById(orderId);

            order.IsPaid = true;
            order.EffectiveDeliveryDate = DateTime.Now;

            return OrderDb.UpdateOrder(order);
        }

        public int CancelOrder(Order order)
        {
            order.IsCancel = true;
            return OrderDb.UpdateOrder(order);
        }
    }
}
