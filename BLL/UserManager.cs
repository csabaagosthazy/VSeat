using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class UserManager:IUserManager
    {
        private IUserDB UserDb { get; }
        private IOrderDB OrderDb { get; }
        private IRestaurantDB RestaurantDb { get; }
        public UserManager(IUserDB userDb, IOrderDB orderDb, IRestaurantDB restaurantDb)
        {
            UserDb = userDb;
            OrderDb = orderDb;
            RestaurantDb = restaurantDb;
        }
        public List<User> GetUsers(){
            return UserDb.GetUsers();
        }
        public User GetUserByEmailAndPassword(string email, string password)
        {
            return UserDb.GetUserByEmailAndPassword(email, password);
        }

        public User GetUserById(int userId)
        {
            return UserDb.GetUserById(userId);
        }
        public List<string> GetUserEmailList()
        {
            List<string> emailList = null;
            var userList = UserDb.GetUsers();
            if(userList != null)
            {
                emailList = new List<string>();
                userList.ForEach(user => emailList.Add(user.Email));
            }
            return emailList;
        }

        public User GetFreeCourierInCity(DateTime deliveryDateTime, int cityId)
        {
            // get all couriers where working city is the same
            User result = null;
            var couriers = UserDb.GetUsers();
            if(couriers == null ) return result;

            couriers = couriers.FindAll(c => c.CityId == cityId && c.Role == "Courier");
            if (couriers == null) return result;
            //get all orders
            //from orders extract restaurant id
            //from orders get delivery times and couriers
            var orderList = OrderDb.GetOrders();

            if(orderList != null)
            {
                List<Order> orders = orderList;
                //get orders from the same city
                List<Order> filtered = new List<Order>();
                foreach(Order order in orders)
                {
                    //check the scheduled time is in range and active order
                    if (order.EffectiveDeliveryDate == null && (order.ScheduledDeliveryDate <= deliveryDateTime.AddMinutes(15) || order.ScheduledDeliveryDate >= deliveryDateTime.AddMinutes(-15)) && !order.IsCancel) 
                    { 
                        Restaurant restaurant = RestaurantDb.GetRestaurantById(order.RestaurantId);
                        if (restaurant.CityId == cityId) filtered.Add(order);
                    }
                }

                if(filtered.Count > 0)
                {
                    //count orders assigned to different couriers
                    //if count is less then 5 assign the courier
                    foreach(User courier in couriers)
                    {
                        int count = filtered.FindAll(f => f.CourierId == courier.CityId).Count;
                        if(count < 5 )
                        {
                            result = courier;
                            break;
                        }
                    }

                }
                else
                {
                    result = couriers[0];
                }
            }
            else
            {
                result = couriers[0];
            }

            return result;


        }

        public User CreateUser(User user)
        {
            return UserDb.CreateUser(user);
        }

        public int DeleteUser(int userId)
        {
            return UserDb.DeleteUser(userId);
        }

        public int ModifyUser(User user)
        {
            return UserDb.UpdateUser(user);
        }
    }
}
