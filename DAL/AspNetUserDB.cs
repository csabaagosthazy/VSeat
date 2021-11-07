using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;

namespace DAL
{
    class AspNetUserDB : IAspNetUserDB
    {
        private IConfiguration Configuration { get; }

        public AspNetUserDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public AspNetUser GetUser(string email, string password)
        {
            AspNetUser user = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from AspNetUser where Email = @email AND PasswordHash = @password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new AspNetUser();

                            user.Id = (String)dr["Id"];

                            if (dr["FirstName"] != null)
                                user.FirstName = (string)dr["FirstName"];

                            if (dr["LastName"] != null)
                                user.LastName = (string)dr["LastName"];

                            if (dr["Email"] != null)
                                user.LastName = (string)dr["LastName"];

                            if (dr["Street"] != null)
                                user.LastName = (string)dr["LastName"];

                            if (dr["StreetNumber"] != null)
                                user.LastName = (string)dr["LastName"];

                            if (dr["PhoneNumber"] != null)
                                user.LastName = (string)dr["LastName"];

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




    }
}
