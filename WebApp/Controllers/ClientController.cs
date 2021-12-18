
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
            var restaurants = RestaurantManager.GetRestaurants();
            return View(restaurants);
        }
        public IActionResult SelectDishes(int id)
        {
            var dishes = DishManager.GetDishesByRestaurantId(id);
            return View(dishes);
        }
        // AddToCart  
        public IActionResult AddToCart(int dishId)
        {
            //if cart list exist in session?
            var cartListJson = HttpContext.Session.GetString("Cart");
            //get dish from db and convert it to cartitem
            var dish = DishManager.GetDishById(dishId);
            CartItem item = new CartItem
            {
                ItemId = dish.DishId,
                ItemName = dish.Name,
                Quantity = 1,
                Price = dish.Price
            };
            if (cartListJson == null)
            {   //create cart list 
                List<CartItem> cart = new List<CartItem>();
                cart.Add(item);
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }
            else
            {
                var cart = JsonConvert.DeserializeObject<List<CartItem>>(cartListJson);
                //check if the item already exist in the cart
                bool exist = cart.Exists(c => c.ItemId == dishId);
                if (!exist)
                {
                    //if item is not on the list -> add it
                    cart.Add(item);
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                }
                else
                {
                    //modify values of the existing item
                    List<CartItem> modifiedCart = new List<CartItem>();
                    foreach (CartItem c in cart)
                    {
                        if (c.ItemId == dishId)
                        {
                            //add quantity 
                            c.Quantity++;
                            //add price
                            c.Price += c.Price;

                        }
                        modifiedCart.Add(c);
                    };

                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(modifiedCart));
                }
            }
            return RedirectToAction(nameof(Cart));
        }
        public ActionResult Cart()
        {
            //if cart list exist in session?
            var cartListJson = HttpContext.Session.GetString("Cart");
            if (cartListJson == null)
            {
                return View(null);
            }
            else
            {
                var cart = JsonConvert.DeserializeObject<List<CartItem>>(cartListJson);
                ViewBag.total = cart.Sum(item => Math.Round(item.Price, 2));
                return View(cart);
            }
        }
        public IActionResult CreateOrder()
        {
            return View();
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
