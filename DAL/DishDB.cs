using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;


namespace DAL
{
    class DishDB : IDishDB
    {
        private IConfiguration Configuration { get; }

        public DishDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Dish> GetDishesByResturantId(int restaurantId)
        {
            List<Dish> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Dishes where RestaurantId = @RestaurantId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Dish>();

                            Dish dish = new Dish();

                            dish.DishId = (int)dr["DishId"];

                            if (dr["Name"] != null)
                                dish.Name = (string)dr["Name"];

                            if (dr["Description "] != null)
                                dish.Description = (string)dr["Description "];

                            if (dr["Price"] != null)
                                dish.Price = (float)dr["Price"];
                            if (dr["IsActive"] != null)
                                dish.IsActive = (bool)dr["IsActive"];
                            if (dr["SpiceLevel"] != null)
                                dish.SpiceLevel = (int)dr["SpiceLevel"];
             
                             dish.RestaurantId = (int)dr[" RestaurantId"];

                            results.Add(dish);
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

        public Dish GetDishById(int dishId)
        {
            Dish dish = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from Dished where DishId = @DishId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DishId", dishId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            dish = new Dish();

                            dish.DishId = (int)dr["DishId"];

                            if (dr["Name"] != null)
                                dish.Name = (string)dr["Name"];

                            if (dr["Description "] != null)
                                dish.Description = (string)dr["Description "];

                            if (dr["Price"] != null)
                                dish.Price = (float)dr["Price"];
                            if (dr["IsActive"] != null)
                                dish.IsActive = (bool)dr["IsActive"];
                            if (dr["SpiceLevel"] != null)
                                dish.SpiceLevel = (int)dr["SpiceLevel"];

                            dish.RestaurantId = (int)dr[" RestaurantId"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return dish;
        }

        public int AddOrder(Dish dish)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert Dishes (Name, Description, Price, IsActive, SpiceLevel, RestaurantId) values (@Name, @Description, @Price, @IsActive, @SpiceLevel, @RestaurantId)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", dish.Name);
                    cmd.Parameters.AddWithValue("@Description", dish.Description);
                    cmd.Parameters.AddWithValue("@Price", dish.Price);
                    cmd.Parameters.AddWithValue("@IsActive", dish.IsActive);
                    cmd.Parameters.AddWithValue("@SpiceLevel", dish.SpiceLevel);
                    cmd.Parameters.AddWithValue("@RestaurantId", dish.RestaurantId);



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

        public int UpdateDish(Dish dish)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update Orders SET Name = @Name, Description = @Description, Price = @Price, IsActive = @IsActive , SpiceLevel = @SpiceLevel, RestaurantId = @RestaurantId where DishId = @DishId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DishId", dish.DishId);
                    cmd.Parameters.AddWithValue("@Name", dish.Name);
                    cmd.Parameters.AddWithValue("@Description", dish.Description);
                    cmd.Parameters.AddWithValue("@Price", dish.Price);
                    cmd.Parameters.AddWithValue("@IsActive", dish.IsActive);
                    cmd.Parameters.AddWithValue("@SpiceLevel", dish.SpiceLevel);
                    cmd.Parameters.AddWithValue("@RestaurantId", dish.RestaurantId);


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
        public int DeleteDish(int dishID)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Dishes where DisId = @DisId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DisId", dishID);


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
