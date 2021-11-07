﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }
    }
}
