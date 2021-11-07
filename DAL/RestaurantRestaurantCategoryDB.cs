using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    class RestaurantRestaurantCategoryDB : IRestaurantRestaurantCategoryDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantRestaurantCategoryDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public RestaurantRestaurantCategory GetCathegoryByRestaurantId(int restaurantId)
        {

            RestaurantRestaurantCategory rrCathegory = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from RestaurantRestaurantCathegories where RestaurantId = @RestaurantId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            rrCathegory = new RestaurantRestaurantCategory();

                            rrCathegory.RestaurantId = (int)dr["RestaurantId"];
                            rrCathegory.CategoryId = (int)dr["CategoryId"];



                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rrCathegory;
        }
        
    }
}
