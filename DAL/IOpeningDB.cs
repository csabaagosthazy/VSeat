using System.Collections.Generic;
using DTO;

namespace DAL
{
    public interface IOpeningDB
    {
        List<Opening> GetOpeningByRestaurantId(int RestaurantId);
        Opening GetOpeningById(int weekday, int restaurandId);
        int AddOpening(Opening opening);
        int UpdateOpening(Opening opening);
        int DeleteOpening(int weekday, int restaurandId);

    }
}
