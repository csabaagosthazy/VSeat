using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Customer : AspNetUser
    {
        public int CustomerId { get; set; }
        public string LoginId { get; set; }
    }
}
