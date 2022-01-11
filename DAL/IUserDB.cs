using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUserDB
    {
        List<User> GetUsers();

        User GetUserById(int userId);
        User GetUserByEmailAndPassword(string email, string password);
        User CreateUser(User user);
        int UpdateUser(User user);
        int DeleteUser(int userId);
    }
}
