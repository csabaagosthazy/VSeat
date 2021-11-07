using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    class RestaurantDB
    {

        private IConfiguration Configuration { get; }
        public RestaurantDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurant>();

                            Restaurant restaurant = new Restaurant();

                            restaurant.RestaurantId = (int)dr["RestaurantId"];

                            if (dr["Name"] != null)
                                restaurant.Name = (string)dr["Name"];

                            if (dr["Phone"] != null)
                                restaurant.Phone = (string)dr["Phone"];


                            restaurant.Email = (string)dr["Email"];

                            restaurant.Street = (string)dr["Street"];

                            restaurant.StreetNumber = (string)dr["StreetNumber"];

                            restaurant.CityId = (int)dr["CityId"];

                            results.Add(restaurant);
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
    }
}
