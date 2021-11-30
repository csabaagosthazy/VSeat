using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL
{
    public interface ICustomerDB
    {
        Customer CreateCustomer(Customer customer);
        Customer GetUser(string email, string password);
    }
    
}
