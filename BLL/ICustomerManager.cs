﻿using System;
using System.Collections.Generic;
using DTO;
using System.Text;

namespace BLL
{
    interface ICustomerManager
    {
        Customer CreateCustomer(Customer customer);
        Customer GetUser(string email, string password);
   
    }
}