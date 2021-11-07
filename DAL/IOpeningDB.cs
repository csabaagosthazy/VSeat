using System.Collections.Generic;

namespace DAL
{
    public interface IOpeningDB
    {
        List<Opening> GetOpeningByRestaurantId(int RestaurantId);
    }
}
