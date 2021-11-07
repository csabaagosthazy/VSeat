using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;

namespace DAL
{
    class CustomersDB
    {
        private IConfiguration Configuration { get; }
        public CustomersDB(IConfiguration configuration)
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
                            customer = new AspNetUser();

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




    }
}

}
