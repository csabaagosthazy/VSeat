using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public DateTime ScheduledDeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool CashPayment { get; set; }
        
    }
}
