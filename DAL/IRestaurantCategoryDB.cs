using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL
{
    public interface IRestaurantCategoryDB
    {
        List<RestaurantCategory> GetRestaurantCategories();
        RestaurantCategory GetRestaurantCategoryById(int categoryId);
        int AddRestaurantCategory(RestaurantCategory rc);
        int UpdateRestaurantCategory(RestaurantCategory rc);
        int DeleteRestaurantCategory(int categoryId);

    }
}
