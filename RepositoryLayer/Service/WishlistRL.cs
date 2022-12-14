using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishlistRL : IWishlistRL
    {
        private readonly IConfiguration configuration;

        public WishlistRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private SqlConnection CreateConnection = null;

        public bool AddToWishlist(int userId, int bookId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"insert into tblWishlist values ({userId},{bookId})";
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

        public List<WishlistModel> ViewWishlist(int userId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                List<WishlistModel> wishlist = new List<WishlistModel>();

                string query = $"select * from tblWishlist where UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        wishlist.Add(new WishlistModel()
                        {
                            WishlistId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            BookId = reader.GetInt32(2)
                        });
                    }

                    return wishlist;
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

        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"delete from tblWishlist where UserId = {userId} and WishlistId = {wishlistId}";
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
    }
}
