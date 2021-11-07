using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IDishDB
    {
        List<Dish> GetDishesByResturantId(int RestaurantId);
    }
}
