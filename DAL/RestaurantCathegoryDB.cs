using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    class RestaurantCathegoryDB : IRestaurantCathegoryDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantCathegoryDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public RestaurantCathegory GetRestaurantCathegory(int id)
        {
            RestaurantCathegory restaurantCathegory = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from RestaurantCathegories where id = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            restaurantCathegory = new RestaurantCathegory();

                            restaurantCathegory.CathegoryID = (int)dr["CathegoryID"];

                            if (dr["name"] != null)
                                restaurantCathegory.name = (string)dr["name"];

                           

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return restaurantCathegory;
        }
    }
}
