using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;




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
                    cmd.CommandType = CommandType.StoredProcedure;

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
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public int AddRestaurant(Restaurant restaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert Restaurants (Name, Phone, Email, StreetNo, Street, City,PostCode) values (@Name, @Phone, @Email, @StreetNo, @Street, @City, @PostCode)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
                    cmd.Parameters.AddWithValue("@Email", restaurant.Email);
                    cmd.Parameters.AddWithValue("@StreetNo", restaurant.StreetNo);
                    cmd.Parameters.AddWithValue("@Street", restaurant.Street);
                    cmd.Parameters.AddWithValue("@City", restaurant.City);
                    cmd.Parameters.AddWithValue("@PostCode", restaurant.PostCode);



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

                    string query = "Update Restaurants SET Name = @Name, Phone = @Phone, Email = @Email, StreetNo = @StreetNo , Street = @Street, City = @City, PostCode = @PostCode  where RestaurantId = @RestaurantId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantID", restaurant.RestaurantID);
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
                    cmd.Parameters.AddWithValue("@Email", restaurant.Email);
                    cmd.Parameters.AddWithValue("@StreetNo", restaurant.StreetNo);
                    cmd.Parameters.AddWithValue("@Street", restaurant.Street);
                    cmd.Parameters.AddWithValue("@City", restaurant.City);
                    cmd.Parameters.AddWithValue("@PostCode", restaurant.PostCode);


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
                    cmd.CommandType = CommandType.StoredProcedure;
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
