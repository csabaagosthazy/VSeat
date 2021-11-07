namespace DAL
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public int OrderId{ get; set; }
        public int DishId { get; set; }

    }
}
