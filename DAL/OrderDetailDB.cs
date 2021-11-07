using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    class OrderDetailDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        ///Create customer
        public OrderDetail CreateOrderDetail(OrderDetail orderDetail)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into AspNetUsers(UnitPrice, Quantity, Discount, OrderId, DishId) values(@UnitPrice, @Quantity, @Discount, @OrderId, @DishId); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    orderDetail.OrderDetailId = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);
                    cmd.Parameters.AddWithValue("@OrderId", orderDetail.OrderId);
                    cmd.Parameters.AddWithValue("@DishId", orderDetail.DishId);

                    cn.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return orderDetail;
        }
    }
}
