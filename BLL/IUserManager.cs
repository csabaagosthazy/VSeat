using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IUserManager
    {
        User GetUserByEmailAndPassword(string email, string password);
        User GetUserById(int userId);
        User GetFreeCourierInCity(DateTime deliveryDateTime, int cityId);
        User CreateUser(User user);
    }
}
