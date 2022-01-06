
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VsEatMVC.Models;
using WebApp.ViewModels;

namespace VsEatMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;

        private IRestaurantManager RestaurantManager { get; }
        private IDishManager DishManager { get; }

        private ICourierManager CourierManager { get; }
        private IOrderManager OrderManager { get; }

        public ClientController(ILogger<ClientController> logger, IRestaurantManager restaurantManager, IDishManager dishManager, ICourierManager courierManager, IOrderManager orderManager)
        {
            _logger = logger;
            RestaurantManager = restaurantManager;
            DishManager = dishManager;
            CourierManager = courierManager;
            OrderManager = orderManager;

        }
        public IActionResult Client()
        {
            
            return View();
        }

        public IActionResult BrowseRestaurants()
        {
            var allRestaurants = RestaurantManager.GetRestaurants();
            List<DTO.Restaurant> result = new List<DTO.Restaurant>();
            var cartJson = HttpContext.Session.GetString("Cart");
            //if the cart is not empty, give only one restaurant which is on the list
            if (cartJson != null)
            {
                //at this point we have only one item, or items with the same restaurant id
                var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                //the list must contain only the cart reasturant
                IEnumerable<DTO.Restaurant> query = allRestaurants.Where(rest => rest.RestaurantId == cart.RestaurantId);
                foreach (DTO.Restaurant restaurant in query)
                {
                    result.Add(restaurant);
                }

            }
            else
            {
                result = allRestaurants;
            }

            return View(result);
        }
        public IActionResult SelectDishes(int id)
        {
            var dishes = DishManager.GetDishesByRestaurantId(id);
            return View(dishes);
        }
        //Restaurant details
        public IActionResult RestaurantDetails (int id)
        {
            var restaurant = RestaurantManager.GetRestaurantById(id);
            return View(restaurant);
        }
        // AddToCart  
        public IActionResult AddToCart(int dishId)
        {
            //if cart list exist in session?
            var cartJson = HttpContext.Session.GetString("Cart");
            //get dish from db and convert it to cartitem
            var dish = DishManager.GetDishById(dishId);
            //cart is bind to restaurant
            var restaurantId = dish.RestaurantId;
            //cart is bind to user
            //get from auth method
            var userId = 1;

            CartItem item = new CartItem
            {
                ItemId = dish.DishId,
                ItemName = dish.Name,
                Quantity = 1,
                Price = Math.Round(dish.Price, 2)
            };
            if (cartJson == null)
            {
                //create cart list 
                List<CartItem> cartList = new List<CartItem>();
                cartList.Add(item);
                //create cart
                Cart cart = new Cart()
                {
                    UserId = userId,
                    RestaurantId = restaurantId,
                    Discount = 0,
                    TotalPrice = item.Price,
                    Items = cartList
                };
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }
            else
            {
                //get cart
                var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                //check if the item already exist in the cart
                bool exist = cart.Items.Exists(c => c.ItemId == dishId);
                if (!exist)
                {
                    //if item is not on the list -> add it
                    cart.Items.Add(item);
                    cart.TotalPrice += item.Price;
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                }
                else
                {
                    //modify values of the existing item
                    List<CartItem> modifiedCartList = new List<CartItem>();
                    foreach (CartItem c in cart.Items)
                    {
                        if (c.ItemId == dishId)
                        {
                            //add quantity 
                            c.Quantity++;
                            //add price
                            cart.TotalPrice += c.Price;
                            c.Price += c.Price;
                        }
                        modifiedCartList.Add(c);
                    };

                    cart.Items = modifiedCartList;
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                }
            }
            return RedirectToAction(nameof(Cart));
        }
        //Remove cart item
        public IActionResult RemoveCartItem(int cartItemId)
        {
            //get cart
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            //filter removed id
            List<CartItem> cartList = new List<CartItem>();
            var modifiedList = cart.Items.Where(cartItem => cartItem.ItemId != cartItemId);
            if(modifiedList.Count() == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                decimal newTotalPrice = 0;
                foreach (CartItem cartItem in modifiedList)
                {
                    cartList.Add(cartItem);
                    newTotalPrice += cartItem.Price;
                }
                cart.Items = cartList;
                cart.TotalPrice = newTotalPrice;
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            }

            return RedirectToAction(nameof(Cart));
        }
        public IActionResult Cart()
        {
            //if cart list exist in session?
            var cartJson = HttpContext.Session.GetString("Cart");
            if (cartJson == null)
            {
                return View(null);
            }
            else
            {
                var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                ViewBag.total = Math.Round(cart.TotalPrice, 2);
                return View(cart.Items);
            }
        }
        public IActionResult CreateOrder()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            //create order object
            OrderDetails order = new OrderDetails
            {
                OrderNumber = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice,
                ScheduledDeliveryDate = DateTime.Now,
                CashPayment = false,
                IsPaid = false,
                CustomerId = cart.UserId,
                RestaurantId = cart.RestaurantId,
                OrderItems = cart.Items

            };
            //create datalist for dropdown menu
            DateTime minOrderDate = DateTime.Today.AddHours(8).AddMinutes(30);
            DateTime maxOrderDate = DateTime.Today.AddHours(20).AddMinutes(30);
            DateTime openHour = DateTime.Today.AddHours(9);
            //before 8:30 -> from 9 today
            //calculating start hour
            if (DateTime.Now > minOrderDate && DateTime.Now < maxOrderDate)
            {
                //get closest 15 min divider over 30 mins
                int hours = DateTime.Now.Hour;
                int minutes = DateTime.Now.Minute;

                if (minutes <= 15) 
                {                 
                    minutes = 45;
                } 
                else if (minutes > 15 && minutes <= 30) 
                { 
                    minutes = 0;
                    hours++;
                } 
                else if(minutes > 30 && minutes <= 45)
                {
                    minutes = 15;
                    hours++;
                }
                else if (minutes > 45 && minutes <= 59)
                {
                    minutes = 30;
                    hours++;
                }

                openHour = DateTime.Today.AddHours(hours).AddMinutes(minutes);
            }
            if(DateTime.Now > maxOrderDate)
            {
                openHour = DateTime.Today.AddDays(1).AddHours(9);
                maxOrderDate = DateTime.Today.AddDays(1).AddHours(21);
            }
            //create list 
            List<DateTime> dateList = new List<DateTime>();
            while (openHour <= maxOrderDate)
            {
                dateList.Add(openHour);
                openHour = openHour.AddMinutes(15);
            }

            ViewBag.dateList = dateList;

            //show cart items and total price
            //select delivery date and time
            //select payment type -> if immediate pay-> cash payment
            return View(order);
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderDetails orderDetails)
        {
            //get free couriers
            int cityId = RestaurantManager.GetRestaurantById(orderDetails.RestaurantId).CityId;
            Courier courier = CourierManager.GetFreeCourierInCity(orderDetails.ScheduledDeliveryDate, cityId);
            if(courier == null)
            {
                return RedirectToAction("OrderError", new { message = "There is no available courier!" });
            }
            //save order to database
            Order order = new Order
            {
                OrderNumber = orderDetails.OrderNumber,
                OrderDate = orderDetails.OrderDate,
                ScheduledDeliveryDate = orderDetails.ScheduledDeliveryDate,
                TotalPrice = (decimal)orderDetails.TotalPrice,
                CashPayment = orderDetails.CashPayment,
                IsPaid = orderDetails.IsPaid,
                IsCancel = false,
                CustomerId = orderDetails.CustomerId,
                CourierId = courier.Id
            };
            List<OrderDetail> itemList = new List<OrderDetail>();
            foreach(CartItem item in orderDetails.OrderItems)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    UnitPrice = (decimal)item.Price,
                    Quantity = item.Quantity,
                    Discount = 0,
                    DishId = item.ItemId
                };

                itemList.Add(orderDetail);

            }
            OrderManager.CreateOrder(order, itemList);
            return RedirectToAction(nameof(Orders));
        }

        public IActionResult OrderError(string message)
        {
            ViewBag.message = message;
            return View();
        }

        public IActionResult Orders()
        {
            //get user id (auth)
            int userId = 1;
            //get active user orders
            List<Order> orders = new List<Order>();
            foreach(Order order in OrderManager.GetOrderByUserId(userId))
            {
                if (!order.IsCancel && order.EffectiveDeliveryDate == null) orders.Add(order);
            }

            return View(orders);
        }

        public IActionResult ModifyOrder()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
