using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IOrderDetailManager
    {
        OrderDetail CreateOrderDetail(OrderDetail orderDetail);
    }
}
