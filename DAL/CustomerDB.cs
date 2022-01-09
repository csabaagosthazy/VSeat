using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class CustomerDB : ICustomerDB
    {
        private IConfiguration Configuration { get; }
        public CustomerDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Customer GetUser(string email, string password)
        {
            Customer customer = null;

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
                            customer = new Customer();

                            customer.Id = (String)dr["Id"];

                            if (dr["FirstName"] != null)
                                customer.FirstName = (string)dr["FirstName"];

                            if (dr["LastName"] != null)
                                customer.LastName = (string)dr["LastName"];

                            if (dr["Email"] != null)
                                customer.LastName = (string)dr["LastName"];

                            if (dr["Street"] != null)
                                customer.LastName = (string)dr["LastName"];

                            if (dr["StreetNumber"] != null)
                                customer.LastName = (string)dr["LastName"];

                            if (dr["PhoneNumber"] != null)
                                customer.LastName = (string)dr["LastName"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;
        }

        ///Create customer
        public Customer CreateCustomer(Customer customer)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //customer.Id = Guid.NewGuid().ToString();
 
            //Create the Customer with the AspNetUser data
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Customers(CustomerId, LoginId) values(@LoginId1, @LoginId); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@LoginId", customer.LoginId);
                    cmd.Parameters.AddWithValue("@LoginId1", customer.CustomerId);

                    

                    cn.Open();
                    customer.CustomerId = cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;
        }




    }
}


