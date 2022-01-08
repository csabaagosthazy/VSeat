using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class OrderDetails
    {
        public long OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ScheduledDeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool CashPayment { get; set; }
        public bool IsPaid { get; set; } = false;
        public string CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public List<CartItem> OrderItems { get; set; }
    }
}
