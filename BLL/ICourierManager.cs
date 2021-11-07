using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface ICourierManager
    {
        Courier GetUser(string email, string password);
    }
}
