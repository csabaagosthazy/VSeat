
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VsEatMVC.Models;

namespace VsEatMVC.Controllers
{
    public class CourierController : Controller
    {
        private readonly ILogger<CourierController> _logger;

        public CourierController(ILogger<CourierController> logger)
        {
            _logger = logger;
        }
        public IActionResult Courier()
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
