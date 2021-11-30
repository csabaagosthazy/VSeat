using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrderDetailManager
    {
        private IOrderDetailDB OrderDetailDb { get; }
        private IOrderDB OrderDb { get; }
        public OrderDetailManager(IOrderDetailDB orderDetailDb, IOrderDB orderDb)
        {
            OrderDetailDb = orderDetailDb;
            OrderDb = orderDb;
        }

        public OrderDetail CreateOrderDetail(int orderId, OrderDetail orderDetail)
        {
            return OrderDetailDb.InsertOrderDetail(orderId,orderDetail);
        }
    }
}
