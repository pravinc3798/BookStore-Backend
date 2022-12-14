using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configurations;
        private static SqlConnection CreateConnection = null;

        public UserRL(IConfiguration configurations)
        {
            this.configurations = configurations;
        }

        public UserModel AddUser(UserModel userModel)
        {
            try
            {
                CreateConnection = new SqlConnection(configurations.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"insert into tblUser values ('{userModel.FullName}','{userModel.ContactNumber}','{userModel.EmailId}','{Encrypt(userModel.UserPassword)}')";
                SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);
                sqlcommand.ExecuteNonQuery();

                return userModel;
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

        public string Login(string emailId, string userPassword)
        {
            try
            {
                CreateConnection = new SqlConnection(configurations.GetConnectionString("BookStore"));
                CreateConnection.Open();

                var encryptedPass = Encrypt(userPassword);

                string query = $"select * from tblUser where EmailId='{emailId}' and UserPassword='{encryptedPass}'";
                SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);
                var userId = Convert.ToInt64(sqlcommand.ExecuteScalar());

                if (userId != 0)
                {
                    var token = GenerateSecurityToken(emailId, userId);
                    return token;
                }
                else
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

        public string ForgetPassword(string emailId)
        {
            try
            {
                CreateConnection = new SqlConnection(configurations.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"select * from tblUser where EmailId='{emailId}'";
                SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);
                var userId = Convert.ToInt64(sqlcommand.ExecuteScalar());

                if (userId != 0)
                {
                    var token = GenerateSecurityToken(emailId, userId);
                    MSMQModel msmq = new MSMQModel();
                    msmq.sendData2Queue(token);
                    return token;
                }
                else
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

        public bool ResetPassword(string emailId, string userPassword, string confirmPassword)
        {
            try
            {
                if (confirmPassword != userPassword) throw new Exception();

                SqlConnection CreateConnection = new SqlConnection(configurations.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"select * from tblUser where EmailId = '{emailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                var userId = Convert.ToInt64(sqlCommand.ExecuteScalar());

                if (userId != 0)
                {
                    query = $"update tblUser set UserPassword = '{Encrypt(userPassword)}' where EmailId = '{emailId}'";
                    sqlCommand = new SqlCommand(query, CreateConnection);
                    sqlCommand.ExecuteNonQuery();

                    return true;
                }
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

        private string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configurations[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        private static readonly string mask = "safhajkfh28934iowqrf@#42@#$";
        private static string Encrypt(string pass)
        {
            if (pass == null) return "";

            pass += mask;

            var encodedPass = Encoding.UTF8.GetBytes(pass);
            return Convert.ToBase64String(encodedPass);
        }

        private static string Decrypt(string encodedPass)
        {
            if (encodedPass == null) return "";

            var encodedBytes = Convert.FromBase64String(encodedPass);
            var decodedPass = Encoding.UTF8.GetString(encodedBytes);
            return decodedPass.Substring(0, decodedPass.Length - mask.Length);
        }
    }
}
