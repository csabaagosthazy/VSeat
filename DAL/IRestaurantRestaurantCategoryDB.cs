using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IRestaurantRestaurantCategoryDB
    {
        RestaurantRestaurantCategory GetCathegoryByRestaurantId(int restaurantId);
    }
}
