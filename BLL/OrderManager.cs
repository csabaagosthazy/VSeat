using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrderManager
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
            OrderDb.CreateOrder(order);
            foreach(var orderDetail in orderDetails)
            {
                OrderDetailDb.InsertOrderDetail(order.OrderId, orderDetail);
            }
            

            return order;

        }

    }
}
