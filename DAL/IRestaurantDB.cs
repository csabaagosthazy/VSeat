using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IRestaurantDB
    {
        
        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurantById(int id);
        //Restaurant AddRestaurant(Restaurant restaurant);
        //Restaurant UpdateRestaurant(Restaurant restaurant);
        //Restaurant DeleteRestaurant(int id);
    }

}
