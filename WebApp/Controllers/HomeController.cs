using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using VsEatMVC.Models;
using WebApp.ViewModels;

namespace VsEatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserManager UserManager { get; }
        private ICityManager CityManager { get; }

        public HomeController(ILogger<HomeController> logger, IUserManager userManager, ICityManager cityManager)
        {
            _logger = logger;
            UserManager = userManager;
            CityManager = cityManager;
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                //get user from db
                User loginUser = UserManager.GetUserByEmailAndPassword(loginVM.Email, loginVM.Password);
                if (loginUser != null)
                {
                    //get role and id and save it to session
                    HttpContext.Session.SetInt32("UserID", loginUser.UserId);
                    HttpContext.Session.SetString("UserRole", loginUser.Role);

                    //redirect to the right page
                    if(loginUser.Role == "Admin") return RedirectToAction("Admin", "Admin");
                    if (loginUser.Role == "Courier") return RedirectToAction("Courier", "Courier");
                    else return RedirectToAction("Client", "Client");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return View(loginVM);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Home");
        }

        public IActionResult Register()
        {

            List<City> cities = CityManager.GetCities();

            SelectList list = new SelectList(cities, "CityId", "Name");
            ViewBag.cities = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM registerVM)
        {

            if (ModelState.IsValid)
            {
                //create user
                User user = new User
                {
                    Role = "Client",
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.Email,
                    CityId = registerVM.CityId,
                    Street = registerVM.Street,
                    StreetNumber = registerVM.StreetNumber,
                    Password = registerVM.Password
                };

                var createdUser = UserManager.CreateUser(user);
                HttpContext.Session.SetInt32("UserID", createdUser.UserId);
                HttpContext.Session.SetString("UserRole", createdUser.Role);
                if(createdUser != null) return RedirectToAction("Client", "Client");
            }
            ModelState.AddModelError(string.Empty, "Registration failed!");

            return View(registerVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserGuide()
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
