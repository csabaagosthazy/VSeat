using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class OrderDB : IOrderDB
    {
        private IConfiguration Configuration { get; }
        public OrderDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        ///Create customer
        public Order CreateOrder(Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Orders(OrderNumber, OrderDate, ScheduledDeliveryDate, TotalPrice,CashPayment, IsPaid, IsCancel, CustomerId, CourierId, RestaurantId) values(@OrderNumber, @OrderDate, @ScheduledDeliveryDate, @TotalPrice, @CashPayment, @IsPaid, @IsCancel, @CustomerId, @CourierId, @RestaurantId); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    
                    order.OrderId = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderId);
                    cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ScheduledDeliveryDate", order.ScheduledDeliveryDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@CashPayment", order.CashPayment);
                    cmd.Parameters.AddWithValue("@IsPaid", order.IsPaid);
                    cmd.Parameters.AddWithValue("@IsCancel", order.IsCancel);
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@CourierId", order.CourierId);
                    cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);

                    cn.Open();



                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }

        public List<Order> GetOrders()
        {
            List<Order> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Orders";
                    SqlCommand cmd = new SqlCommand(query, cn);

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
                                order.TotalPrice = (decimal)dr["TotalPrice"];
                            if (dr["CashPayment"] != null)
                                order.CashPayment = (bool)dr["CashPayment"];
                            if (dr["IsPaid"] != null)
                                order.IsPaid = (bool)dr["IsPaid"];
                            if (dr["IsCancel"] != null)
                                order.IsCancel = (bool)dr["IsCancel"];
                            if (dr["CustomerId"] != null)
                                order.CustomerId = (int)dr["CustomerId"];
                            if (dr["CourierId"] != null)
                                order.CourierId = (string)dr["CourierId"];
                            if (dr["RestaurantId"] != null)
                                order.RestaurantId = (int)dr["RestaurantId"];

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
                    string query = "Select from Orders where OrderId = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
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
                                order.TotalPrice = (decimal)dr["TotalPrice"];
                            if (dr["CashPayment"] != null)
                                order.CashPayment = (bool)dr["CashPayment"];
                            if (dr["IsPaid"] != null)
                                order.IsPaid = (bool)dr["IsPaid"];
                            if (dr["IsCancel"] != null)
                                order.IsCancel = (bool)dr["IsCancel"];
                            if (dr["CustomerId"] != null)
                                order.CustomerId = (int)dr["CustomerId"];
                            if (dr["CourierId"] != null)
                                order.CourierId = (string)dr["CourierId"];
                            if (dr["RestaurantId"] != null)
                                order.RestaurantId = (int)dr["RestaurantId"];

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

        public int AddOrder(Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert Orders (OrderNumber, OrderDate, ScheduledDeliveryDate, TotalPrice, CashPayment, IsPaid, IsCancel, DeliveryAddressId,  CustomerId, CourierId, RestaurantId ) values (@OrderNumber, @OrderDate, @ScheduledDeliveryDate, @TotalPrice, @CashPayment, @IsPaid, @IsCancel, @DeliveryAddressId,  @CustomerId, @CourierId. @RestaurantId )";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@ScheduledDeliveryDate", order.ScheduledDeliveryDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@CashPayment", order.CashPayment);
                    cmd.Parameters.AddWithValue("@IsPaid", order.IsPaid);
                    cmd.Parameters.AddWithValue("@IsCancel", order.IsCancel);
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@CourierId", order.CourierId);
                    cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);


                    cn.Open();
                    result = cmd.ExecuteNonQuery();
                    cn.Close();
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            return result;

        }

        public int UpdateOrder(Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update Orders SET OrderNumber = @OrderNumber, OrderDate = @OrderDate, ScheduledDeliveryDate = @ScheduledDeliveryDate, TotalPrice = @TotalPrice , CashPayment = @CashPayment, IsPaid = @IsPaid, IsCancel = @IsCancel, DeliveryAddressId = @DeliveryAddressId,  CustomerId = @CustomerId, CourierId = @CourierId, RestaurantId = @RestaurantId where OrderId = @OrderId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@ScheduledDeliveryDate", order.ScheduledDeliveryDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@CashPayment", order.CashPayment);
                    cmd.Parameters.AddWithValue("@IsPaid", order.IsPaid);
                    cmd.Parameters.AddWithValue("@IsCancel", order.IsCancel);
                    cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@CourierId", order.CourierId);
                    cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);


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
        public int DeleteOrder(int id)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Orders where @OrderId = id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);


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
