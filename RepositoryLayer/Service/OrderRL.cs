using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        private readonly IConfiguration configuration;

        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private SqlConnection CreateConnection = null;

        public OrderModel AddOrder(OrderModel orderModel)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("spAddOrder", CreateConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                orderModel.OrderDate = DateTime.Now.Date;
                sqlCommand.Parameters.AddWithValue("@cartId", orderModel.CartId);
                sqlCommand.Parameters.AddWithValue("@addressId", orderModel.AddressId);
                sqlCommand.Parameters.AddWithValue("@orderDate", orderModel.OrderDate);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    orderModel.Quantity = reader.GetInt32(1);
                    orderModel.TotalPrice = orderModel.Quantity * (float)reader.GetDouble(0);
                    return orderModel;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CreateConnection.Close();
            }
        }

        public List<OrderModel> ViewOrders(int userId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                List<OrderModel> listOfOrders = new List<OrderModel>();
                string query = $"select OrderId, C.CartId, AddressId, Orderdate, CartQty, CartQty * DiscountedPrice as totalPrice " +
                    $"from tblOrder as O inner join tblCart as C on O.CartId = C.CartId inner join tblBook as B on C.BookId = B.BookId where UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listOfOrders.Add(new OrderModel()
                        {
                            OrderId = reader.GetInt32(0),
                            CartId = reader.GetInt32(1),
                            AddressId = reader.GetInt32(2),
                            OrderDate = reader.GetDateTime(3),
                            Quantity = reader.GetInt32(4),
                            TotalPrice = (float)reader.GetDouble(5)
                        });
                    }
                    return listOfOrders;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CreateConnection.Close();
            }
        }

        public bool CancelOrder(int orderId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("spCancelOrder", CreateConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@orderId", orderId);

                int result = sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CreateConnection.Close();
            }
        }
    }
}
