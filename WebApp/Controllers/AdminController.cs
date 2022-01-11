using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using VsEatMVC.Models;
using WebApp.ViewModels;

namespace VsEatMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private IAdminManager AdminManager { get; }
        private IUserManager UserManager { get; }

        public AdminController(ILogger<AdminController> logger, IAdminManager adminManager, IUserManager userManager)
        {
            _logger = logger;
            AdminManager = adminManager;
            UserManager = userManager;
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult ManageUsers()
        {
            List<DTO.User> users = UserManager.GetUsers();

            List<UserVM> userList = new List<UserVM>();

            users.ForEach(user => userList.Add(new UserVM { UserId = user.UserId, UserRole = user.Role }));

            return View(userList);
        }
        public IActionResult DeleteUser(int userId)
        {
            UserManager.DeleteUser(userId);
            return RedirectToAction("Admin", "ManageUsers");
        }
        public IActionResult ModifyUser(int userId, string role)
        {
            DTO.User user = UserManager.GetUserById(userId);
            user.Role = role;

            UserManager.ModifyUser(user);
            return RedirectToAction("Admin", "ManageUsers");
        }
        public IActionResult ManageDatabase(string result)
        {
            
            ViewBag.message = result;
            return View();
        }
        public IActionResult ResetDatabase()
        {
            int queryResult = AdminManager.ResetDatabase();
            string message = queryResult == 1 ? "Database successfully reseted" : "Database reset failed";
            return RedirectToAction("ManageDatabase", new { result = message});
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
