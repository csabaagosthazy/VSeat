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
            List<Dish> dishes = new List<Dish>();
            Dish dish1 = new Dish();

            dish1.DishId = 1;
            dish1.Name = "dish1";
            dish1.Description = "Desc1";
            dish1.Price = (float)1.3;
            dish1.IsActive = true;
            dish1.SpiceLevel = 1;
            dish1.RestaurantId = id;

            dishes.Add(dish1);

            Dish dish2 = new Dish();
            dish2.DishId = 2;
            dish2.Name = "dish2";
            dish2.Description = "Desc2";
            dish2.Price = (float)1.4;
            dish2.IsActive = true;
            dish2.SpiceLevel = 2;
            dish2.RestaurantId = id;

            dishes.Add(dish2);

            return dishes;
            //return DishDB.GetDishesByResturantId(id);
        }

        public Dish GetDishById(int dishId)
        {

            Dish dish1 = new Dish
            {
                DishId = 1,
                Name = "dish",
                Description = "Desc",
                Price = (float)1.3,
                IsActive = true,
                SpiceLevel = 1,
                RestaurantId = 1
            };

            Dish dish2 = new Dish
            {
                DishId = 2,
                Name = "dish2",
                Description = "Desc2",
                Price = (float)1.4,
                IsActive = true,
                SpiceLevel = 2,
                RestaurantId = 1
            };


            if (dishId == 1) return dish1;

            return dish2;
            //return DishDB.GetDishById(dishId);
        }
    }
}
