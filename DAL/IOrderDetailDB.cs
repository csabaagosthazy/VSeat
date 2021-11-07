using DTO;

namespace DAL
{
    public interface IOrderDetailDB
    {
        OrderDetail InsertOrderDetail(int orderId, OrderDetail orderDetail);
        List<OrderDetail> GetOrderItemsByOrderId(int orderId);
        OrderDetail GetOrderItemById(int orderDetailId);
        int AddOrderDetail(OrderDetail orderDetail);
        int UpdateOrderDetail(OrderDetail orderDetail);
        int DeleteOrderDetail(int orderDetailId);

    }
}