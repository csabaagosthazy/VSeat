using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;



namespace DAL
{
    public class RestaurantDB : IRestaurantDB
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

        public Restaurant GetRestaurantById(int id)
        {
            Restaurant restaurant = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where id = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            restaurant = new Restaurant();

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


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return restaurant;
        }


    }

    
}
