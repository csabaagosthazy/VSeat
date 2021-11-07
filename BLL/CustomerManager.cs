using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerDB CustomerDb { get; }
        public CustomerManager(IConfiguration conf)
        {
            CustomerDb = new CustomerDB(conf);
        }

        public Customer CreateCustomer(Customer customer)
        {
            return CustomerDb.CreateCustomer(customer);
        }

        public Customer GetUser(string email, string password)
        {
            return CustomerDb.GetUser(email, password);
        }
    }
}
