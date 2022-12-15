using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private static SqlConnection CreateConnection = null;
        private readonly IConfiguration configuration;

        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddAdministrator(string administrator, string adminPassword)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"insert into tblAdmin values ('{administrator}','{Encrypt(adminPassword)}')";
                SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);
                sqlcommand.ExecuteNonQuery();

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

        public string Login(string administrator, string adminPassword)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                var encryptedPass = Encrypt(adminPassword);

                string query = $"select * from tblAdmin where Administrator='{administrator}' and AdminPassword='{encryptedPass}'";
                SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);
                var adminId = Convert.ToInt64(sqlcommand.ExecuteScalar());

                if (adminId != 0)
                {
                    var token = GenerateSecurityToken(administrator, adminId);
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

        private string GenerateSecurityToken(string administrator, long adminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, administrator),
                    new Claim("AdminId", adminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        private static readonly string mask = "sa@#42@#$";
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
