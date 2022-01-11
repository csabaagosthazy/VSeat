using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AdminDB : IAdminDB
    {
        private IConfiguration Configuration { get; }

        public AdminDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public int ResetDatabase()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();        
                    SqlCommand cmd = new SqlCommand("ResetDatabase", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Close();
                    result = 1;

                }


            }
            catch
            {
                result = 0;
            }
            return result;
        }
    }
}
