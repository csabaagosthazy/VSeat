using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RestaurantManager : IRestaurantManager
    {
        private IRestaurantDB RestaurantDB { get; }
        private ICityDB CityDb { get; }

        public RestaurantManager(IRestaurantDB restaurantDB, ICityDB cityDb)
        {
            RestaurantDB = restaurantDB;
            CityDb = cityDb;
        }
        public List<Restaurant> GetRestaurants()
        {


            return RestaurantDB.GetRestaurants();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return RestaurantDB.GetRestaurantById(id);
        }
    }
}
