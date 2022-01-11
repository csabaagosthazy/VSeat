
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VsEatMVC.Models;
using WebApp.ViewModels;

namespace VsEatMVC.Controllers
{
    public class CourierController : Controller
    {
        private readonly ILogger<CourierController> _logger;

        private IOrderManager OrderManager { get; }
        private IUserManager UserManager { get; }
        private ICityManager CityManager { get; }

        public CourierController(ILogger<CourierController> logger, IOrderManager orderManager, IUserManager userManager, ICityManager cityManager)
        {
            _logger = logger;
            OrderManager = orderManager;
            UserManager = userManager;
            CityManager = cityManager;
        }

        public IActionResult Courier()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null || HttpContext.Session.GetString("UserRole") != "Courier")
            {
                return RedirectToAction("Home", "Login");
            }
            return View();
        }
        public IActionResult Orders()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null || HttpContext.Session.GetString("UserRole") != "Courier")
            {
                return RedirectToAction("Home", "Login");
            }
            List<OrderVM> orders = null;
            //get courier orders
            var courierOrders = OrderManager.GetOrdersByCourierId((int) userId);

            if(courierOrders != null)
            {

                orders = new List<OrderVM>();
                foreach (DTO.Order dr in courierOrders)
                {
                    if(!dr.IsCancel && dr.EffectiveDeliveryDate == null)
                    {
                        //get customer details
                        DTO.User customer = UserManager.GetUserById(dr.CustomerId);
                        DTO.City city = CityManager.GetCityById(customer.CityId);

                        OrderVM vm = new OrderVM
                        {
                            OrderId = (int)dr.OrderId,
                            ScheduledDeliveryDate = dr.ScheduledDeliveryDate,
                            City = city.Name,
                            Street = customer.Street,
                            StreetNumber = customer.StreetNumber,
                            ClientName = customer.FirstName + " " + customer.LastName,
                            TotalPrice = dr.TotalPrice,
                            IsPayed = dr.IsPaid,
                            CashPayment = dr.CashPayment                    
                        };
                        orders.Add(vm);
                    }

                } 
                if(orders.Count == 0)
                {
                    return View(null);
                }
            }

            return View(orders);
        }
        public IActionResult DeliverOrder(int orderId)
        {
            OrderManager.DeliverOrderById(orderId);

            return RedirectToAction(nameof(Orders));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
