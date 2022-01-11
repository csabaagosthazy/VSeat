using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class UserDB : IUserDB
    {

        private IConfiguration Configuration { get; }
        public UserDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<User> GetUsers()
        {
            List<User> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Users";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<User>();

                            User user = new User();

                            user.UserId = (int)dr["UserId"];
                            user.Role = (string)dr["Role"];
                            user.FirstName = (string)dr["FirstName"];
                            user.LastName = (string)dr["LastName"];
                            user.Email = (string)dr["Email"];
                            user.Password = (string)dr["Password"];
                            user.CityId = (int)dr["CityId"];
                            user.Street = (string)dr["Street"];
                            user.StreetNumber = (string)dr["StreetNumber"];

                            results.Add(user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }

        public User GetUserById(int userId)
        {
            User user = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Users where UserId = @UserId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            user = new User
                            {
                                UserId = (int)dr["UserId"],
                                Role = (string)dr["Role"],
                                FirstName = (string)dr["FirstName"],
                                LastName = (string)dr["LastName"],
                                Email = (string)dr["Email"],
                                Password = (string)dr["Password"],
                                CityId = (int)dr["CityId"],
                                Street = (string)dr["Street"],
                                StreetNumber = (string)dr["StreetNumber"]
                            };

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return user;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            User user = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Users where Email = @Email AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            user = new User
                            {
                                UserId = (int)dr["UserId"],
                                Role = (string)dr["Role"],
                                FirstName = (string)dr["FirstName"],
                                LastName = (string)dr["LastName"],
                                Email = (string)dr["Email"],
                                Password = (string)dr["Password"],
                                CityId = (int)dr["CityId"],
                                Street = (string)dr["Street"],
                                StreetNumber = (string)dr["StreetNumber"]
                            };

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return user;
        }
        public User CreateUser(User user)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users(Role, FirstName, LastName, Email, Password, CityId, Street, StreetNumber) VALUES (@Role, @FirstName, @LastName, @Email, @Password, @CityId, @Street, @StreetNumber); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@CityId", user.CityId);
                    cmd.Parameters.AddWithValue("@Street", user.Street);
                    cmd.Parameters.AddWithValue("@StreetNumber", user.StreetNumber);

                    cn.Open();

                    user.UserId = Convert.ToInt32(cmd.ExecuteScalar());



                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return user;
        }
        public int UpdateUser(User user)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update Users SET Role = @Role, FirstName = @FirstName, LastName = @LastName, Email = @Email , Password = @Password, CityId = @CityId, Street = @Street, StreetNumber = @StreetNumber where USerId = @UserId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@CityId", user.CityId);
                    cmd.Parameters.AddWithValue("@Street", user.Street);
                    cmd.Parameters.AddWithValue("@StreetNumberd", user.StreetNumber);


                    cn.Open();
                    result = cmd.ExecuteNonQuery();

                }


            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public int DeleteUser(int userId)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Users where @UserId = userId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cn.Open();
                    result = cmd.ExecuteNonQuery();

                }


            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
