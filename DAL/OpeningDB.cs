using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

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
                    cmd.CommandType = CommandType.StoredProcedure;
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
        public Opening GetOpeningById(int weekday, int restaurandId)
        {
            Opening opening = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from Opening where Weekday = @Weekday and RestaurandId = @RestaurandId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Weekday", weekday);
                    cmd.Parameters.AddWithValue("@RestaurandId", restaurandId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            opening = new Opening();

                            opening.WeekDay = (int)dr["WeekDay"];

                            if (dr["OpenHour"] != null)
                                opening.OpenHour = (DateTime)dr["OpenHour"];

                            if (dr["CloseHour"] != null)
                                opening.CloseHour = (DateTime)dr["CloseHour"];

                            if (dr["PauseStart"] != null)
                                opening.PauseStart = (DateTime)dr["PauseStart"];
                            if (dr["PauseEnd"] != null)
                                opening.PauseEnd = (DateTime)dr["PauseEnd"];

                            opening.RestaurantId = (int)dr["RestaurantId"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return opening;
        }

        public int AddOpening(Opening opening)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert Opening (Weekday, OpenHour, CloseHour, PauseStart, PauseEnd, RestaurantId) values (@Weekday, @OpenHour, @CloseHour, @PauseStart, @PauseEnd, @RestaurantId)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Weekday", opening.WeekDay);
                    cmd.Parameters.AddWithValue("@OpenHour", opening.OpenHour);
                    cmd.Parameters.AddWithValue("@CloseHour", opening.CloseHour);
                    cmd.Parameters.AddWithValue("@PauseStart", opening.PauseStart);
                    cmd.Parameters.AddWithValue("@PauseEnd", opening.PauseEnd);
                    cmd.Parameters.AddWithValue("@RestaurantId", opening.RestaurantId);



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

        public int UpdateOpening(Opening opening)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update Opening SET Weekday = @Weekday, OpenHour = @OpenHour, CloseHour = @CloseHour, PauseStart = @PauseStart , PauseEnd = @PauseEnd, RestaurantId = @RestaurantId where Weekday = @Weekday and RestaurantId = @RestaurantId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Weekday", opening.WeekDay);
                    cmd.Parameters.AddWithValue("@OpenHour", opening.OpenHour);
                    cmd.Parameters.AddWithValue("@CloseHour", opening.CloseHour);
                    cmd.Parameters.AddWithValue("@PauseStart", opening.PauseStart);
                    cmd.Parameters.AddWithValue("@PauseEnd", opening.PauseEnd);
                    cmd.Parameters.AddWithValue("@RestaurantId", opening.RestaurantId);


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
        public int DeleteOpening(int weekday, int restaurandId)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Opening where Weekday = @Weekday and RestaurandId = @RestaurandId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Weekday", weekday);
                    cmd.Parameters.AddWithValue("@RestaurandId", restaurandId);


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
