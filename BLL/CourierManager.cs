using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CourierManager:ICourierManager
    {
        private ICourierDB CourierDb { get; }
        public CourierManager(IConfiguration conf)
        {
            CourierDb = new CourierDB(conf);
        }

        public Courier GetUser(string email, string password)
        {
            return CourierDb.GetUser(email, password);
        }
    }
}
