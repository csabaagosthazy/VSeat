using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;
namespace DAL
{
    class RestaurantRestaurantCategoryDB : IRestaurantRestaurantCategoryDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantRestaurantCategoryDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<RestaurantRestaurantCategory> GetRestaurantRestaurantCategories()
        {
            List<RestaurantRestaurantCategory> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (results == null)
                        results = new List<RestaurantRestaurantCategory>();

                    string query = "Select from RestaurantRestaurantCategories";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            RestaurantRestaurantCategory rrc = new RestaurantRestaurantCategory();

                            rrc.CategoryId = (int)dr["CategoryId"];

                         
                            rrc.RestaurantId = (int)dr["RestaurantId"];

                            results.Add(rrc);

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

        public RestaurantRestaurantCategory GetCategoryByRestaurantId(int restaurantId)
        {

            RestaurantRestaurantCategory rrCathegory = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from RestaurantRestaurantCategories where RestaurantId = @RestaurantId";
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
        public int AddRestaurantRestaurantCategory(RestaurantRestaurantCategory rrc)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert RestaurantRestaurantCategories (RestaurantId,CategoryId ) values (@RestaurantId, @CategoryId)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantId", rrc.RestaurantId);
                    cmd.Parameters.AddWithValue("@CategoryId", rrc.CategoryId);




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

        public int UpdateRestaurantRestaurantCategory(RestaurantRestaurantCategory rrc)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update RestaurantRestaurantCategories SET RestaurantId = @RestaurantId, CategoryId = @CategoryId where RestaurantId = @RestaurantId and CategoryId = @CategoryId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantId", rrc.RestaurantId);
                    cmd.Parameters.AddWithValue("@CategoryId", rrc.CategoryId);



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
        public int DeleteRestaurantRestaurantCategory(int restaurantId, int categoryId)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM RestaurantCathegories where RestaurantId = @RestaurantId and CategoryId = @CategoryId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);


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
