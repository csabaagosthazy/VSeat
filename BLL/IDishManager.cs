using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IDishManager
    {
        List<Dish> GetDishesByRestaurantId(int id);
        Dish GetDishById(int dishId);
    }
}
