using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DTO;

namespace DAL
{
    public class RestaurantsDB : IRestaurantsDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Restaurants> GetRestaurants()
        {
            List<Restaurants> results = null;
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
                                results = new List<Restaurants>();

                            Restaurants restaurant = new Restaurants();

                            restaurant.RestaurantID = (int)dr["RestaurantID"];

                            if (dr["Name"] != null)
                                restaurant.Name = (string)dr["Name"];

                            if (dr["Phone"] != null)
                                restaurant.Phone = (string)dr["Phone"];

                            if (dr["Email"] != null)
                                restaurant.Email = (string)dr["Email"];
                            if (dr["StreetNo"] != null)
                                restaurant.StreetNo = (int)dr["StreetNo"];
                            if (dr["Street"] != null)
                                restaurant.Street = (string)dr["Street"];
                            if (dr["City"] != null)
                                restaurant.City = (string)dr["City"];
                            if (dr["PostCode"] != null)
                                restaurant.PostCode = (int)dr["PostCode"];


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
