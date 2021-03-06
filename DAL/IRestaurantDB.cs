
using System.Collections.Generic;
using DTO;


namespace DAL
{
    public interface IRestaurantDB
    {
        
        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurantById(int id);
        int AddRestaurant(Restaurant restaurant);
        int UpdateRestaurant(Restaurant restaurant);
        int DeleteRestaurant(int id);
    }

}
