using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    class RestaurantCategoryDB : IRestaurantCategoryDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantCategoryDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<RestaurantCategory> GetRestaurantCategories()
        {
            List<RestaurantCategory> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (results == null)
                        results = new List<RestaurantCategory>();

                    string query = "Select from RestaurantCategories";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            RestaurantCategory rc = new RestaurantCategory();

                            rc.CategoryID = (int)dr["CategoryID"];

                            if (dr["Name"] != null)
                                rc.Name = (string)dr["Name"];

                            results.Add(rc);

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
        public RestaurantCategory GetRestaurantCategoryById(int cathegoryId)
        {
            RestaurantCategory rc = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from RestaurantCategories where CathegoryId = @CategoryId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", cathegoryId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {



                            rc.CategoryID = (int)dr["CathegoryID"];

                            if (dr["name"] != null)
                                rc.Name = (string)dr["Name"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rc;
        }

        public int AddRestaurantCategory(RestaurantCategory rc)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert RestaurantCategories (Name) values (@Name)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", rc.Name);




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

        public int UpdateRestaurantCategory(RestaurantCategory rc)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update RestaurantCategories SET Name = @Name where CathegoryId = @CathegoryId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", rc.CategoryID);
                    cmd.Parameters.AddWithValue("@Name", rc.Name);



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
        public int DeleteRestaurantCategory(int cathegoryId)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM RestaurantCategories where CategoryId = @CategoryId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", cathegoryId);


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
