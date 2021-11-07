using DTO;

namespace DAL
{
    public interface IOrderDB
    {
        Order CreateOrder(Order order);
    }
}