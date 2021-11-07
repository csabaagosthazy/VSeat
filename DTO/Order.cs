using System;

namespace DAL
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ScheduledDeliveryDate { get; set; }
        public DateTime EffectiveDeliveryDate { get; set; }

        public float TotalPrice { get; set; }

        public bool CashPayment { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCancel { get; set; }
        public int DeliveryAddressId { get; set; }
        public int CustomerId { get; set; }
        public int CourierId { get; set; }

    }
}
