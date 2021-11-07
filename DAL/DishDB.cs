using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    class DishDB : IDishDB
    {
        private IConfiguration Configuration { get; }

        public DishDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Dish> GetDishesByResturantId(int RestaurantId)
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
                    cmd.Parameters.AddWithValue("@RestaurantId", RestaurantId);

                    cn.Open();


                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Dish>();

                            Dish dish = new Dish();

                            dish.DishId = (int)dr["DishID"];

                            if (dr["Name"] != null)
                                dish.Name = (string)dr["Name"];

                            if (dr["Description"] != null)
                                dish.Description = (string)dr["Description"];

                            if (dr["Price"] != null)
                                dish.Price = (float)dr["Price"];
                            if (dr["IsActive"] != null)
                                dish.IsActive = (bool)dr["IsActive"];
                            if (dr["SpiceLevel"] != null)
                                dish.SpiceLevel = (int)dr["SpiceLevel"];
                            if (dr["RestaurantId"] != null)
                                dish.RestaurantId = (int)dr["RestaurantId"];
                          

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
    }
}
