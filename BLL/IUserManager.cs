using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IUserManager
    {
        List<User> GetUsers();
        User GetUserByEmailAndPassword(string email, string password);
        User GetUserById(int userId);
        List<string> GetUserEmailList();
        User GetFreeCourierInCity(DateTime deliveryDateTime, int cityId);
        User CreateUser(User user);

        int DeleteUser(int userId);

        int ModifyUser(User user);
    }
}
