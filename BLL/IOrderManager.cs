using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IOrderManager
    {
        Order CreateOrder(Order order);
    }
}
