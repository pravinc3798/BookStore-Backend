using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly IConfiguration configuration;

        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private SqlConnection CreateConnection = null;

        public bool AddAddress(int userId, AddressModel model)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"insert into tblAddress values ({userId},{model.AddressTypeId}, '{model.MainAddress}', " +
                    $"'{model.City}', '{model.State}', '{model.Zip}')";
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

        public bool UpdateAddress(int userId, AddressModel model)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"update tblAddress set UserId = {userId}, AddressTypeId = {model.AddressTypeId}, MainAddress = '{model.MainAddress}', " +
                    $"City = '{model.City}', State = '{model.State}', Zip = '{model.Zip}' where AddressId = {model.AddressId}";
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

        public List<AddressModel> GetAllAddresses(int userId)
        {
            try
            {
                List<AddressModel> addressList = new List<AddressModel>();
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"select * from tblAddress where UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query,CreateConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        addressList.Add(new AddressModel()
                        {
                            AddressId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            AddressTypeId = reader.GetInt32(2),
                            MainAddress = reader.GetString(3),
                            City = reader.GetString(4),
                            State = reader.GetString(5),
                            Zip = reader.GetString(6)
                        });
                    }
                    return addressList;
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

        public bool RemoveAddress(int userId, int addressId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"delete from tblAddress where UserId = {userId} and AddressId = {addressId}";
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
