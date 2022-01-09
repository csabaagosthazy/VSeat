using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CourierManager:ICourierManager
    {
        private ICourierDB CourierDb { get; }
        private IOrderDB OrderDb { get; }
        private IRestaurantDB RestaurantDb { get; }
        public CourierManager(ICourierDB courierDb, IOrderDB orderDb, IRestaurantDB restaurantDb)
        {
            CourierDb = courierDb;
            OrderDb = orderDb;
            RestaurantDb = restaurantDb;
        }

        public Courier GetCourier(string email, string password)
        {
            return CourierDb.GetUser(email, password);
        }

        public Courier GetFreeCourierInCity(DateTime deliveryDateTime, int cityId)
        {
            // get all couriers where working city is the same
            Courier result = null;
            var couriers = CourierDb.GetCouriers();
            if(couriers == null ) return result;

            couriers = couriers.FindAll(c => c.WorkingCityId == cityId);
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
                    foreach(Courier courier in couriers)
                    {
                        int count = filtered.FindAll(f => f.CourierId.Equals(courier.CourierId)).Count;
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

        public Courier CreateCourier(Courier courier)
        {
            return CourierDb.CreateCourier(courier);
        }
    }
}
