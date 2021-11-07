
namespace DTO
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; } = 0;
        public int OrderId { get; set; }
        public int DishId { get; set; }
    }
}
