using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class OrderVM
    {
        [DisplayName("Id")]
        public int OrderId { get; set; }
        [DisplayName("Due date")]
        public DateTime ScheduledDeliveryDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        [DisplayName("Customer name")]
        public string ClientName { get; set; }
        [DisplayName("Price")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Payed")]
        public bool IsPayed{ get; set; }
        [DisplayName("Cash payment")]
        public bool CashPayment { get; set; }

    }
}
