using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class OrderDetailDB : IOrderDetailDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       

        public OrderDetail InsertOrderDetail(int orderId, OrderDetail orderDetail)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into OrderDetails(UnitPrice, Quantity, Discount, OrderId, DishId) values(@UnitPrice, @Quantity, @Discount, @OrderId, @DishId); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    orderDetail.OrderDetailId = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
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
        public List<OrderDetail> GetOrderItemsByOrderId(int orderId)
        {
            List<OrderDetail> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from OrderDetails where OrderId = @orderId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<OrderDetail>();

                            OrderDetail orderDetail = new OrderDetail();

                            orderDetail.OrderDetailId = (int)dr["OrderDetailId"];

                            if (dr["UnitPrice"] != null)
                                orderDetail.UnitPrice = (float)dr["UnitPrice"];

                            if (dr["Quantity"] != null)
                                orderDetail.Quantity = (int)dr["Quantity"];

                            if (dr["Discount"] != null)
                                orderDetail.Discount = (float)dr["Discount"];

                            orderDetail.OrderId = (int)dr["OrderId"];

                            orderDetail.DishId = (int)dr["DishId"];

                            results.Add(orderDetail);
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

        public OrderDetail GetOrderItemById(int orderDetailId)
        {
            OrderDetail orderDetail = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from OrderDetails where OrderDetailId = @orderDetailId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderDetailId", orderDetailId);

                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            orderDetail = new OrderDetail();

                            orderDetail.OrderDetailId = (int)dr["OrderDetailId"];

                            if (dr["UnitPrice"] != null)
                                orderDetail.UnitPrice = (float)dr["UnitPrice"];

                            if (dr["Quantity"] != null)
                                orderDetail.Quantity = (int)dr["Quantity"];

                            if (dr["Discount"] != null)
                                orderDetail.Discount = (float)dr["Discount"];

                            orderDetail.OrderId = (int)dr["OrderId"];

                            orderDetail.DishId = (int)dr["DishId"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return orderDetail;
        }

        public int AddOrderDetail(OrderDetail orderDetail)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert OrderDetails (UnitPrice, Quantity, Discount, OrderId, DishId) values (@UnitPrice, @Quantity, @Discount, @OrderId, @DishId)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);
                    cmd.Parameters.AddWithValue("@OrderId", orderDetail.OrderId);
                    cmd.Parameters.AddWithValue("@DishId", orderDetail.DishId);




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

        public int UpdateOrderDetail(OrderDetail orderDetail)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "Update OrderDeatails SET UnitPrice = @UnitPrice, Quantity = @Quantity, Discount = @Discount, OrderId = @OrderId , DishId = @DishId where OrderDetailId = @OrderDetailId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderDetailId", orderDetail.OrderDetailId);
                    cmd.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);
                    cmd.Parameters.AddWithValue("@OrderId", orderDetail.OrderId);
                    cmd.Parameters.AddWithValue("@DishId", orderDetail.DishId);


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
        public int DeleteOrderDetail(int orderDetailId)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM OrderDetails where OrderDetailId = @orderDetailId";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderDetailId", orderDetailId);


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
