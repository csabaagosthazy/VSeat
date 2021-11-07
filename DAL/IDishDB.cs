using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL
{
    public interface IDishDB
    {
        List<Dish> GetDishesByResturantId(int RestaurantId);
        Dish GetDishById(int dishId);
        int AddOrder(Dish dish);
        int UpdateDish(Dish dish);
        int DeleteDish(int dishID);

    }
}
