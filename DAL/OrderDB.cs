using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    class OrderDB
    {
        private IConfiguration Configuration { get; }
        public OrderDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        ///Create customer
        public Order CreateCustomer(Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
           
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into AspNetUsers(OrderNumber, OrderDate, ScheduledDeliveryDate, TotalPrice,CashPayment, IsPaid, IsCancel, CustomerId, CourierId) values(@OrderNumber, @OrderDate, @ScheduledDeliveryDate, @TotalPrice, @CashPayment, @IsPaid, @IsCancel @CustomerId, @CourierId); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderId);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@ScheduledDeliveryDate", order.ScheduledDeliveryDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@CashPayment", order.CashPayment);
                    cmd.Parameters.AddWithValue("@IsPaid", order.IsPaid);
                    cmd.Parameters.AddWithValue("@IsCancel", order.IsCancel);
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@CourierId", order.CourierId);
                        

                    cn.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }
    }
}
