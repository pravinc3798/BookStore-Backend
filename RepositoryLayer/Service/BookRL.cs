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
    public class BookRL : IBookRL
    {
		SqlConnection CreateConnection = null;

        private IConfiguration configuration;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(BookModel bookModel)
        {
			try
			{
				CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
				CreateConnection.Open();

				string query = $"insert into tblBook values('{bookModel.BookName}','{bookModel.AuthorName}','{bookModel.Description}','{bookModel.Image}','{bookModel.Ratings}','{bookModel.Reviews}','{bookModel.DiscountedPrice}','{bookModel.OriginalPrice}','{bookModel.Quantity}')";
				SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);

				int result = sqlcommand.ExecuteNonQuery();

				if (result > 0) return bookModel;
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

		public BookModel EditBook(BookModel bookModel)
		{
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"update tblBook set BookName = '{bookModel.BookName}', AuthorName = '{bookModel.AuthorName}', " +
                    $"BookDescription = '{bookModel.Description}', BookImage = '{bookModel.Image}', " +
                    $"Ratings = '{bookModel.Ratings}', Reviews = '{bookModel.Reviews}', " +
                    $"DiscountedPrice = '{bookModel.DiscountedPrice}', OriginalPrice = '{bookModel.OriginalPrice}', " +
                    $"Quantity = '{bookModel.Quantity}' where BookId = {bookModel.BookId}";

                SqlCommand sqlcommand = new SqlCommand(query, CreateConnection);

                int result = sqlcommand.ExecuteNonQuery();

                if (result > 0) return bookModel;
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

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();
            
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = "select * from tblBook";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        BookModel bookModel = PopulateBookModel(dataReader);
                        books.Add(bookModel);
                    }
                    return books;
                }

                else return null;
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

        public bool DeleteBook(int bookId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"Delete from tblBook where BookId = {bookId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                var result = sqlCommand.ExecuteNonQuery();
                
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

        public BookModel GetBookById (int bookId)
        {
            try
            {
                CreateConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                CreateConnection.Open();

                string query = $"select * from tblBook where BookId = {bookId}";
                SqlCommand sqlCommand = new SqlCommand(query, CreateConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while(reader.Read())
                {
                    BookModel bookModel = PopulateBookModel(reader);
                    return bookModel;
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

        private BookModel PopulateBookModel(SqlDataReader dataReader)
        {
            BookModel bookModel = new BookModel()
            {
                BookId = dataReader.GetInt32(0),
                BookName = dataReader.GetString(1),
                AuthorName = dataReader.GetString(2),
                Description = dataReader.GetString(3),
                Image = dataReader.GetString(4),
                Ratings = (float)dataReader.GetDouble(5),
                Reviews = dataReader.GetInt32(6),
                DiscountedPrice = (float)dataReader.GetDouble(7),
                OriginalPrice = (float)dataReader.GetDouble(8),
                Quantity = dataReader.GetInt32(9)
            };
            return bookModel;
        }
    }
}
