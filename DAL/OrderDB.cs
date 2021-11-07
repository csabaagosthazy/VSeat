using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class OrderDB : IOrderDB
    {
        private IConfiguration Configuration { get; }

        public OrderDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Order> GetOrders()
        {
            List<Order> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Dishes";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Order>();

                            Order order = new Order();

                            order.OrderId = (int)dr["OrderId"];

                            if (dr["OrderNumber"] != null)
                                order.OrderNumber = (int)dr["OrderNumber"];

                            if (dr["OrderDate"] != null)
                                order.OrderDate = (DateTime)dr["OrderDate"];

                            if (dr["ScheduledDeliveryDate"] != null)
                                order.ScheduledDeliveryDate = (DateTime)dr["ScheduledDeliveryDate"];
                            if (dr["TotalPrice"] != null)
                                order.TotalPrice = (float)dr["TotalPrice"];
                            if (dr["CashPayment"] != null)
                                order.CashPayment = (bool)dr["CashPayment"];
                            if (dr["IsPaid"] != null)
                                order.IsPaid = (bool)dr["IsPaid"];
                            if (dr["IsCancel"] != null)
                                order.IsCancel = (bool)dr["IsCancel"];
                            if (dr["DeliveryAddressId"] != null)
                                order.DeliveryAddressId = (int)dr["DeliveryAddressId"];
                            if (dr["CustomerId"] != null)
                                order.CustomerId = (int)dr["CustomerId"];
                            if (dr["CourierId"] != null)
                                order.CourierId = (int)dr["CourierId"];

                            results.Add(order);
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

        public Order GetOrderById(int id)
        {
            Order order = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select from Dishes where id = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            order = new Order();

                            order.OrderId = (int)dr["OrderId"];

                            if (dr["OrderNumber"] != null)
                                order.OrderNumber = (int)dr["OrderNumber"];

                            if (dr["OrderDate"] != null)
                                order.OrderDate = (DateTime)dr["OrderDate"];

                            if (dr["ScheduledDeliveryDate"] != null)
                                order.ScheduledDeliveryDate = (DateTime)dr["ScheduledDeliveryDate"];
                            if (dr["TotalPrice"] != null)
                                order.TotalPrice = (float)dr["TotalPrice"];
                            if (dr["CashPayment"] != null)
                                order.CashPayment = (bool)dr["CashPayment"];
                            if (dr["IsPaid"] != null)
                                order.IsPaid = (bool)dr["IsPaid"];
                            if (dr["IsCancel"] != null)
                                order.IsCancel = (bool)dr["IsCancel"];
                            if (dr["DeliveryAddressId"] != null)
                                order.DeliveryAddressId = (int)dr["DeliveryAddressId"];
                            if (dr["CustomerId"] != null)
                                order.CustomerId = (int)dr["CustomerId"];
                            if (dr["CourierId"] != null)
                                order.CourierId = (int)dr["CourierId"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }

        public void AddOrder(Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert Orders (OrderNumber, OrderDate, ScheduledDeliveryDate, TotalPrice, CashPayment, IsPaid, IsCancel, DeliveryAddressId,  CustomerId, CourierId ) values (@OrderNumber, @OrderDate, @ScheduledDeliveryDate, @TotalPrice, @CashPayment, @IsPaid, @IsCancel, @DeliveryAddressId,  @CustomerId, @CourierId )";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@ScheduledDeliveryDate", order.ScheduledDeliveryDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@CashPayment", order.CashPayment);
                    cmd.Parameters.AddWithValue("@IsPaid", order.IsPaid);
                    cmd.Parameters.AddWithValue("@IsCancel", order.IsCancel);
                    cmd.Parameters.AddWithValue("@DeliveryAddressId", order.DeliveryAddressId);
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@CourierIdl", order.CourierId);


                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }

                  
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void UpdateOrder(int id, Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    
                    string query = "Update Orders SET OrderNumber = @OrderNumber, OrderDate = @OrderDate, ScheduledDeliveryDate = @ScheduledDeliveryDate, TotalPrice = @TotalPrice , CashPayment = @CashPayment, IsPaid = @IsPaid, IsCancel = @IsCancel, DeliveryAddressId = @DeliveryAddressId,  CustomerId = @CustomerId, CourierId = @CourierId where @id = id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@ScheduledDeliveryDate", order.ScheduledDeliveryDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@CashPayment", order.CashPayment);
                    cmd.Parameters.AddWithValue("@IsPaid", order.IsPaid);
                    cmd.Parameters.AddWithValue("@IsCancel", order.IsCancel);
                    cmd.Parameters.AddWithValue("@DeliveryAddressId", order.DeliveryAddressId);
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@CourierIdl", order.CourierId);


                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void DeleteOrder(int id)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Orders where @id = id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);


                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
  
}
