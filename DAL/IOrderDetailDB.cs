using DTO;

namespace DAL
{
    public interface IOrderDetailDB
    {
        OrderDetail InsertOrderDetail(int orderId, OrderDetail orderDetail);
    }
}