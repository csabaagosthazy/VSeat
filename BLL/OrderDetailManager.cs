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
        public OrderDetailManager(IConfiguration conf)
        {
            OrderDetailDb = new OrderDetailDB(conf);
            OrderDb = new OrderDB(conf);
        }

        public OrderDetail CreateOrderDetail(int orderId, OrderDetail orderDetail)
        {
            return OrderDetailDb.InsertOrderDetail(orderId,orderDetail);
        }
    }
}
