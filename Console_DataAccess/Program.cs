using System;
using System.IO;
using DTO;
using BLL;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Console_DataAccess
{
    class Program
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var customerManager = new CustomerManager(Configuration);
            var orderManager = new OrderManager(Configuration); 
            var orderDetailManager = new OrderDetailManager(Configuration);


            List<OrderDetail> orderDetailList = new List<OrderDetail>();
            orderDetailList.Add(new OrderDetail { UnitPrice = 20, Quantity = 1, OrderId = 0, DishId = 3 });
            
            var newOrder = orderManager.CreateOrder(new Order { ScheduledDeliveryDate = DateTime.Today, TotalPrice = 30, CustomerId=1, CourierId=1 },orderDetailList);
               

            //var newCustomer = customerManager.CreateCustomer(new Customer { FirstName = "Raphael", LastName = "Balmori", Email = "raph.balmo@gmail.com", Street = "Route des papillons", StreetNumber = "21", PasswordHash = "HhnEio", PhoneNumber = "078/9877321", CityId = 1 });
            //Console.WriteLine("Client crée : "+newCustomer.FirstName);




        }
    }
}
