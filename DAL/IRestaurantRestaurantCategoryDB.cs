using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IRestaurantRestaurantCategoryDB
    {
        List<RestaurantRestaurantCategory> GetRestaurantRestaurantCategories();
        RestaurantRestaurantCategory GetCategoryByRestaurantId(int restaurantId);

        int AddRestaurantRestaurantCategory(RestaurantRestaurantCategory rrc);
        int UpdateRestaurantRestaurantCategory(RestaurantRestaurantCategory rrc);
        int DeleteRestaurantRestaurantCategory(int restaurantId, int categoryId);
    }
}
