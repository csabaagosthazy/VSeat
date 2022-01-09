using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class CityDB : ICityDB
    {
        private IConfiguration Configuration { get; }

        public CityDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<City> GetCities()
        {
            List<City> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Cities";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    
                    
                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<City>();

                            City city = new City();

                            city.CityId = (int)dr["CityId"];

                            if (dr["Name"] != null)
                                city.Name = (string)dr["Name"];

                            if (dr["ZipCode"] != null)
                                city.ZipCode = (int)dr["ZipCode"];                    

                         
                            results.Add(city);
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
