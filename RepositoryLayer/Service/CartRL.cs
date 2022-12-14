using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        private readonly IConfiguration configuration;
        private SqlConnection CreateConnection = null;

        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddToCart(int userId, int bookId, int cartQty)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"insert into tblCart values({userId},{bookId},{cartQty})";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                int result = sqlCommand.ExecuteNonQuery();

                if (result > 0) return true;
                return false;
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

        public bool UpdateCart(int cartQty, int userId, int cartId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"update tblCart set CartQty = {cartQty} where CartId = {cartId} and UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                int result = sqlCommand.ExecuteNonQuery();

                if (result > 0) return true;
                return false;
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

        public bool DeleteFromCart(int userId, int cartId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"delete from tblCart where CartId = {cartId} and UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                int result = sqlCommand.ExecuteNonQuery();

                if (result > 0) return true;
                return false;
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

        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                List<CartModel> cart = new List<CartModel>();

                string query = $"select * from tblCart where UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while(dataReader.Read())
                    {
                        cart.Add(new CartModel()
                        {
                            CartId = dataReader.GetInt32(0),
                            UserId = dataReader.GetInt32(1),
                            BookId = dataReader.GetInt32(2),
                            CartQty = dataReader.GetInt32(3)
                        });
                    }
                    return cart;
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
    }
}
