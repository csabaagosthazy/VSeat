using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
namespace DAL
{
    class OpeningDB : IOpeningDB
    {
        private IConfiguration Configuration { get; }

        public OpeningDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Opening> GetOpeningByRestaurantId(int RestaurantId)
        {
            List<Opening> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Openings where RestaurantId = @RestaurantId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@RestaurantId", RestaurantId);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Opening>();

                            Opening opening = new Opening();

                            opening.WeekDay = (int)dr["WeekDay"];

                            if (dr["OpenHour"] != null)
                                opening.OpenHour = (DateTime)dr["OpenHour"];

                            if (dr["CloseHour"] != null)
                                opening.CloseHour = (DateTime)dr["CloseHour"];

                            if (dr["PauseStart"] != null)
                                opening.PauseStart = (DateTime)dr["PauseStart"];
                            if (dr["PauseEnd"] != null)
                                opening.PauseEnd = (DateTime)dr["PauseEnd"];
                            if (dr["RestaurantId"] != null)
                                opening.RestaurantId = (int)dr["RestaurantId"];

                            results.Add(opening);

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
