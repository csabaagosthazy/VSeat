using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

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
            List<Restaurant> results = new List<Restaurant>();
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

                            if (dr["Email"] != null)
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
        public Restaurant GetRestaurantById(int id)
        {
            Restaurant restaurant = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants where RestaurantId = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            restaurant = new Restaurant();

                            restaurant.RestaurantId = (int)dr["RestaurantId"];

                            if (dr["Name"] != null)
                                restaurant.Name = (string)dr["Name"];

                            if (dr["Phone"] != null)
                                restaurant.Phone = (string)dr["Phone"];

                            if (dr["Email"] != null)
                                restaurant.Email = (string)dr["Email"];
                            if (dr["StreetNumber"] != null)
                                restaurant.StreetNumber = (String)dr["StreetNumber"];
                            if (dr["Street"] != null)
                                restaurant.Street = (string)dr["Street"];
                            if (dr["CityId"] != null)
                                restaurant.CityId = (int)dr["CityId"];



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

        public int AddRestaurant(Restaurant restaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert Into Restaurants (Name, Phone, Email, CityId, Street, StreetNumber) values (@Name, @Phone, @Email, @StreetNumber, @Street, @CityId)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
                    cmd.Parameters.AddWithValue("@Email", restaurant.Email);
                    cmd.Parameters.AddWithValue("@StreetNumber", restaurant.StreetNumber);
                    cmd.Parameters.AddWithValue("@Street", restaurant.Street);
                    cmd.Parameters.AddWithValue("@CityId", restaurant.CityId);             

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

        public int UpdateRestaurant(Restaurant restaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update Restaurants SET Name = @Name, Phone = @Phone, Email = @Email, StreetNo = @StreetNo , Street = @Street, CityId = @CityId  where RestaurantId = @RestaurantId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@RestaurantID", restaurant.RestaurantId);
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
                    cmd.Parameters.AddWithValue("@Email", restaurant.Email);
                    cmd.Parameters.AddWithValue("@StreetNo", restaurant.StreetNumber);
                    cmd.Parameters.AddWithValue("@Street", restaurant.Street);
                    cmd.Parameters.AddWithValue("@CityId", restaurant.CityId);                    


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
        public int DeleteRestaurant(int restaurantId)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Restaurants where RestaurandId = @RestaurandId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@RestaurandId", restaurantId);

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
