using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DishManager : IDishManager
    {
        private IDishDB DishDB { get; }

        public DishManager(IDishDB dishDB)
        {
            DishDB = dishDB;
        }
        public List<Dish> GetDishesByRestaurantId(int id)
        {
      
            return DishDB.GetDishesByResturantId(id);
        }

        public Dish GetDishById(int dishId)
        {
            return DishDB.GetDishById(dishId);
        }
    }
}
