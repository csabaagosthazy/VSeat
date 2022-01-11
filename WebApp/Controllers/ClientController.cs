
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
        private IUserManager UserManager { get; }
        private IOrderManager OrderManager { get; }
        private ICityManager CityManager { get; }

        public ClientController(ILogger<ClientController> logger, IRestaurantManager restaurantManager, IDishManager dishManager, IUserManager userManager, IOrderManager orderManager, ICityManager cityManager)
        {
            _logger = logger;
            RestaurantManager = restaurantManager;
            DishManager = dishManager;
            UserManager = userManager;
            OrderManager = orderManager;
            CityManager = cityManager;

        }
        public IActionResult Client()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Home", "Login");
            }

            User user = UserManager.GetUserById((int)userId);

            ViewBag.name = user.FirstName;
            return View();
        }

        public IActionResult BrowseRestaurants()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            //get restaurants form db
            var allRestaurants = RestaurantManager.GetRestaurants();
            //create view model list
            List<RestaurantVM> vmRestaurants = new List<RestaurantVM>();
            foreach(DTO.Restaurant dr in allRestaurants)
            {
                //get city name an postal code
                var city = CityManager.GetCityById(dr.CityId);
                RestaurantVM vm = new RestaurantVM
                {
                    RestaurantId = dr.RestaurantId,
                    Name = dr.Name,
                    Phone = dr.Phone,
                    Email = dr.Email,
                    City = city.Name,
                    ZipCode = city.ZipCode,
                    Street = dr.Street,
                    StreetNumber = dr.StreetNumber
                };
                vmRestaurants.Add(vm);

            }
            List<RestaurantVM> result = new List<RestaurantVM>();
            var cartJson = HttpContext.Session.GetString("Cart");
            //if the cart is not empty, give only one restaurant which is on the list
            if (cartJson != null)
            {
                //at this point we have only one item, or items with the same restaurant id
                var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                //the list must contain only the cart reasturant
                IEnumerable<RestaurantVM> query = vmRestaurants.Where(rest => rest.RestaurantId == cart.RestaurantId);
                foreach (RestaurantVM restaurant in query)
                {
                    result.Add(restaurant);
                }

            }
            else
            {
                result = vmRestaurants;
            }

            return View(result);
        }
        public IActionResult SelectDishes(int id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            var dishes = DishManager.GetDishesByRestaurantId(id);
            return View(dishes);
        }
        //Restaurant details
        public IActionResult RestaurantDetails (int id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            var restaurant = RestaurantManager.GetRestaurantById(id);
            var city = CityManager.GetCityById(restaurant.CityId);
            RestaurantVM vm = new RestaurantVM
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                Phone = restaurant.Phone,
                Email = restaurant.Email,
                City = city.Name,
                ZipCode = city.ZipCode,
                Street = restaurant.Street,
                StreetNumber = restaurant.StreetNumber
            };
            return View(vm);
        }
        // AddToCart  
        public IActionResult AddToCart(int dishId)
        {
            
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            //if cart list exist in session?
            var cartJson = HttpContext.Session.GetString("Cart");
            //get dish from db and convert it to cartitem
            var dish = DishManager.GetDishById(dishId);
            //cart is bind to restaurant
            var restaurantId = dish.RestaurantId;
            //cart is bind to user
            //get from auth method
            var userId = HttpContext.Session.GetInt32("UserID");

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
                    UserId = (int)userId,
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
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Home", "Login");
            }
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
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
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
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            //create order object
            OrderDetailsVM order = new OrderDetailsVM
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
        public IActionResult CreateOrder(OrderDetailsVM orderDetails)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            //get free couriers
            int cityId = RestaurantManager.GetRestaurantById(orderDetails.RestaurantId).CityId;
            User courier = UserManager.GetFreeCourierInCity(orderDetails.ScheduledDeliveryDate, cityId);
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
                RestaurantId = orderDetails.RestaurantId,
                CourierId = courier.UserId
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
            Order res = OrderManager.CreateOrder(order, itemList);
            if (res != null)
            {
                HttpContext.Session.Remove("Cart");
                return RedirectToAction(nameof(Orders));
            }
            else 
            {
                return RedirectToAction("OrderError", new { message = "An error occured!" });
            }
        }

        public IActionResult CancelOrder(long orderId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            //get order to modify
            Order orderToCancel = OrderManager.GetOrderById(orderId);
            //cancel order
            int res = OrderManager.CancelOrder(orderToCancel);

            if(res == 0)
            {
                return RedirectToAction("OrderError", new { message = "Error during order cancellation!" });
            }

            return RedirectToAction(nameof(Orders));
        }

        public IActionResult OrderError(string message)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            ViewBag.message = message;
            return View();
        }

        public IActionResult Orders()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            //get user id (auth)
            var userId = HttpContext.Session.GetInt32("UserID");
            //get active user orders
            List<Order> orders = null;
            var userOrders = OrderManager.GetOrderByUserId((int)userId);
            if (userOrders != null)
            {
                orders = new List<Order>();
                foreach(Order order in userOrders)
                {
                    if (!order.IsCancel && order.EffectiveDeliveryDate == null) orders.Add(order);
                }

            }

            return View(orders);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
