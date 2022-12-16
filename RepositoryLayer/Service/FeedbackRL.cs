using CommonLayer.Model;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL : IFeedbackRL
    {
        private readonly IConfiguration configuration;

        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private SqlConnection CreateConnection = null;

        public FeedbackModel AddFeedback(int userId, int bookId, FeedbackModel model)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"insert into tblFeedback values ({userId}, {bookId}, {model.Ratings}, '{model.Comments}')";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                int result = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (result > 0)
                {
                    model.FeedbackId = result;
                    model.UserId = userId;
                    model.BookId = bookId;
                    return model;
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

        public List<FeedbackModel> MyFeedbacks(int userId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                List<FeedbackModel> feedbacks = new List<FeedbackModel>();

                string query = $"select * from tblFeedback where UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        feedbacks.Add(new FeedbackModel()
                        {
                            FeedbackId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            BookId = reader.GetInt32(2),
                            Ratings = (float)reader.GetDouble(3),
                            Comments = reader.GetString(4)
                        });

                    return feedbacks;
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

        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"delete from tblFeedback where FeedbackId = {feedbackId} and UserId={userId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                int result = sqlCommand.ExecuteNonQuery();

                return (result > 0);
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
