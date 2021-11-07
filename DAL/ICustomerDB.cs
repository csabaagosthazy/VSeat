using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL
{
    interface ICustomerDB:IAspNetUserDB
    {
        Customer CreateCustomer(Customer customer);
    }
    
}
