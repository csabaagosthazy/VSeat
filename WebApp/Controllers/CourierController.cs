
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

        public CourierController(ILogger<CourierController> logger, IOrderManager orderManager)
        {
            _logger = logger;
            OrderManager = orderManager;
        }
        public IActionResult Courier()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Home", "Home");
            }
            var courierOrders = OrderManager.GetOrderByCourierId(HttpContext.Session.GetString("UserID"));
            if (courierOrders == null)
            {
                List<OrderVM> vmOrdersEmpty = new List<OrderVM>();
                OrderVM vmEmpty = new OrderVM
                {
                    OrderId = 0,
                    ScheduledDeliveryDate = DateTime.Now,
                    TotalPrice = 0,
                    CashPayment = false
                };
                vmOrdersEmpty.Add(vmEmpty);
                return View();
            }

            List<OrderVM> vmOrders = new List<OrderVM>();
            foreach (DTO.Order dr in courierOrders)
            {
                    OrderVM vm = new OrderVM
                {
                    OrderId = (int)dr.OrderId,
                    ScheduledDeliveryDate = dr.ScheduledDeliveryDate,
                    TotalPrice = dr.TotalPrice,
                    CashPayment = dr.CashPayment                    
                };
                vmOrders.Add(vm);

            }          

            return View(vmOrders);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        public IActionResult DeliverOrder(int OrderId)
        {

            return RedirectToAction(nameof(Courier));
        }
    }
}
