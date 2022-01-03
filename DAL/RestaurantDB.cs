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
            //mocks
            var test1 = new Restaurant
            {
                RestaurantId = 1,
                Name = (string)"Test1",
                Phone = (string)"123456",
                Email = (string)"a@a.com",
                Street = (string)"Street1",
                StreetNumber = (string)"20",
                City = (string)"Sierre"
            };

            results.Add(test1);

            var test2 = new Restaurant
            {
                City = "Sierre",
                Email = "a@a.com",
                Name = "Test1",
                Phone = "123456",
                RestaurantId = 2,
                Street = "Street2",
                StreetNumber = "21"
            };

            results.Add(test2);

            //try
            //{
            //    using (SqlConnection cn = new SqlConnection(connectionString))
            //    {
            //        string query = "Select * from Restaurants";
            //        SqlCommand cmd = new SqlCommand(query, cn);

            //        cn.Open();

            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                if (results == null)
            //                    results = new List<Restaurant>();

            //                Restaurant restaurant = new Restaurant();

            //                restaurant.RestaurantId = (int)dr["RestaurantId"];

            //                if (dr["Name"] != null)
            //                    restaurant.Name = (string)dr["Name"];

            //                if (dr["Phone"] != null)
            //                    restaurant.Phone = (string)dr["Phone"];


            //                restaurant.Email = (string)dr["Email"];

            //                restaurant.Street = (string)dr["Street"];

            //                restaurant.StreetNumber = (string)dr["StreetNumber"];

            //                restaurant.City = (string)dr["City"];

            //                results.Add(restaurant);
            //            }
            //        }
            //    }
            
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            return results;
        }
        public Restaurant GetRestaurantById(int id)
        {
            Restaurant restaurant = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            //mocks
            var test1 = new Restaurant
            {
                RestaurantId = 1,
                Name = (string)"Test1",
                Phone = (string)"123456",
                Email = (string)"a@a.com",
                Street = (string)"Street1",
                StreetNumber = (string)"20",
                City = (string)"Sierre"
            };

            var test2 = new Restaurant
            {
                City = "Sierre",
                Email = "a@a.com",
                Name = "Test1",
                Phone = "123456",
                RestaurantId = 2,
                Street = "Street2",
                StreetNumber = "21"
            };

            if (id == 1) restaurant = test1;
            else 
            {
                restaurant = test2;
            }

            //try
            //{
            //    using (SqlConnection cn = new SqlConnection(connectionString))
            //    {
            //        string query = "Select * from Restaurants where id = @id";
            //        SqlCommand cmd = new SqlCommand(query, cn);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@id", id);

                //        cn.Open();

                //        using (SqlDataReader dr = cmd.ExecuteReader())
                //        {
                //            while (dr.Read())
                //            {

                //                restaurant = new Restaurant();

                //                restaurant.RestaurantId = (int)dr["RestaurantId"];

                //                if (dr["Name"] != null)
                //                    restaurant.Name = (string)dr["Name"];

                //                if (dr["Phone"] != null)
                //                    restaurant.Phone = (string)dr["Phone"];

                //                if (dr["Email"] != null)
                //                    restaurant.Email = (string)dr["Email"];
                //                if (dr["StreetNo"] != null)
                //                    restaurant.StreetNumber = (String)dr["StreetNumber"];
                //                if (dr["Street"] != null)
                //                    restaurant.Street = (string)dr["Street"];
                //                if (dr["City"] != null)
                //                    restaurant.City = (string)dr["City"];



                //            }
                //        }
                //    }
                //}
                //catch (Exception e)
                //{
                //    throw e;
                //}

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
                    string query = "Insert Restaurants (Name, Phone, Email, StreetNumber, Street, City,PostCode) values (@Name, @Phone, @Email, @StreetNumber, @Street, @CityId)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
                    cmd.Parameters.AddWithValue("@Email", restaurant.Email);
                    cmd.Parameters.AddWithValue("@StreetNumber", restaurant.StreetNumber);
                    cmd.Parameters.AddWithValue("@Street", restaurant.Street);
                    cmd.Parameters.AddWithValue("@City", restaurant.City);             

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
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantID", restaurant.RestaurantId);
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
                    cmd.Parameters.AddWithValue("@Email", restaurant.Email);
                    cmd.Parameters.AddWithValue("@StreetNo", restaurant.StreetNumber);
                    cmd.Parameters.AddWithValue("@Street", restaurant.Street);
                    cmd.Parameters.AddWithValue("@City", restaurant.City);                    


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
