using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    public interface IOrderDetailManager
    {
        OrderDetail CreateOrderDetail(OrderDetail orderDetail);
    }
}
