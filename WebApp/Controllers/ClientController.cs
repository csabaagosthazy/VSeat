
using BLL;
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

        public ClientController(ILogger<ClientController> logger, IRestaurantManager restaurantManager, IDishManager dishManager)
        {
            _logger = logger;
            RestaurantManager = restaurantManager;
            DishManager = dishManager;

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
        public ActionResult RemoveCartItem(int cartItemId)
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
                double newTotalPrice = 0.0;
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
        public ActionResult Cart()
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
                OrderItems = cart.Items

            };
            ViewBag.minDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm");
            ViewBag.maxDate = DateTime.Now.AddDays(2).ToString("yyyy-MM-ddThh:mm");
            //show cart items and total price
            //select delivery date and time
            //select payment type
            return View(order);
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
