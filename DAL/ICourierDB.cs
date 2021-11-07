using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL
{
    public interface ICourierDB
    {
        public Courier GetUser(string email, string password);
    }
}
