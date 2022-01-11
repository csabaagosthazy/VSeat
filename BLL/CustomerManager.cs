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
        public CustomerManager(ICustomerDB customerDb)
        {
            CustomerDb = customerDb;
        }

        public Customer CreateCustomer(Customer customer)
        {
            return CustomerDb.CreateCustomer(customer);
        }

        public Customer GetUser(string email, string password)
        {
            return CustomerDb.GetUser(email, password);
        }

        public Customer GetCustomerById(string userId)
        {
            return CustomerDb.GetCustomerById(userId);
        }
    }
}
