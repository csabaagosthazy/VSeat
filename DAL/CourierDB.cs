using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;

namespace DAL
{
    public class CourierDB : ICourierDB
    {
        private IConfiguration Configuration { get; }
        public CourierDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Courier GetUser(string email, string password)
        {
            Courier courier = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Customers inner join AspNetUsers on Customers.LoginId=AspNetUsers.Id where Email = @email AND PasswordHash = @password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            courier = new Courier();

                            courier.Id = (String)dr["Id"];

                            if (dr["FirstName"] != null)
                                courier.FirstName = (string)dr["FirstName"];

                            if (dr["LastName"] != null)
                                courier.LastName = (string)dr["LastName"];

                            if (dr["Email"] != null)
                                courier.LastName = (string)dr["LastName"];

                            if (dr["Street"] != null)
                                courier.LastName = (string)dr["LastName"];

                            if (dr["StreetNumber"] != null)
                                courier.LastName = (string)dr["LastName"];

                            if (dr["PhoneNumber"] != null)
                                courier.LastName = (string)dr["LastName"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return courier;
        }

        public List<Courier> GetCouriers()
        {
            List<Courier> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Couriers";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Courier>();

                            Courier courier = new Courier();

                            courier.CourierId = (string)dr["CourierId"];

                            if (dr["LoginId"] != null)
                                courier.LoginId = (string)dr["LoginId"];

                            if (dr["WorkingCityId"] != null)
                                courier.WorkingCityId = (int)dr["WorkingCityId"];

                             results.Add(courier);
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

        public Courier CreateCourier(Courier courier)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //courier.Id = Guid.NewGuid().ToString();
            

            //Create the Customer with the AspNetUser data
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Couriers(CourierId,LoginId, WorkingCityId) values(@LoginId1, @LoginId, @WorkingCityId); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@LoginId", courier.LoginId);                  
                    cmd.Parameters.AddWithValue("@LoginId1", courier.CourierId);
                    cmd.Parameters.AddWithValue("@WorkingCityId", courier.WorkingCityId);

                    

                    cn.Open();
                    courier.CourierId = cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return courier;
        }

        
    }
}
