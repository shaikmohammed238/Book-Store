using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL:IBookRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;

        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddBook(BookModel bookModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_AddBooks", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlcmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlcmd.Parameters.AddWithValue("@DiscountPriceValue", bookModel.DiscountPriceValue);
                    sqlcmd.Parameters.AddWithValue("@OriginalPriceValue", bookModel.OriginalPriceValue);
                    sqlcmd.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                    sqlcmd.Parameters.AddWithValue("@TotalRating", bookModel.TotalRating);
                    sqlcmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    sqlcmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    sqlcmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return "Successfully Added book in BookStore";
                    }
                    else
                    {
                        return "check detalis book is not added";
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string UpdateBookDetais(BookModel bookModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_UpdateBooks", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@BookId", bookModel.BookId);
                    sqlcmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlcmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlcmd.Parameters.AddWithValue("@DiscountPriceValue", bookModel.DiscountPriceValue);
                    sqlcmd.Parameters.AddWithValue("@OriginalPriceValue", bookModel.OriginalPriceValue);
                    sqlcmd.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                    sqlcmd.Parameters.AddWithValue("@TotalRating", bookModel.TotalRating);
                    sqlcmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    sqlcmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    sqlcmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return "Successfully updated book in BookStore";
                    }
                    else
                    {
                        return "check this book id ";
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string DeleteBook(long bookId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_DeleteBook", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@BookId",bookId);
                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return "Successfully deleted book from BookStore";
                    }
                    else
                    {
                        return "check this book id ";
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<BookModel> GetBookByBookId(long bookId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    List<BookModel> result = new List<BookModel>();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            BookModel bookModel = new BookModel();

                            bookModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPriceValue = Convert.ToInt32(sqlData["DiscountPriceValue"]);
                            bookModel.OriginalPriceValue = Convert.ToInt32(sqlData["OriginalPriceValue"]);
                            bookModel.BookDescription = sqlData["BookDescription"].ToString();
                            bookModel.TotalRating = Convert.ToInt32(sqlData["TotalRating"]);
                            bookModel.RatingCount = Convert.ToInt32(sqlData["RatingCount"]);
                            bookModel.BookImage = sqlData["BookImage"].ToString();
                            bookModel.BookQuantity = Convert.ToInt32(sqlData["BookQuantity"]);
                            result.Add(bookModel);
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<BookModel> GetAllBooks()
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);

            try
            {
                using (sqlConnection)
                {
                    List<BookModel> result = new List<BookModel>();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetAllBooks", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            BookModel bookModel = new BookModel();

                            bookModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPriceValue = Convert.ToInt32(sqlData["DiscountPriceValue"]);
                            bookModel.OriginalPriceValue = Convert.ToInt32(sqlData["OriginalPriceValue"]);
                            bookModel.BookDescription = sqlData["BookDescription"].ToString();
                            bookModel.TotalRating = Convert.ToInt32(sqlData["TotalRating"]);
                            bookModel.RatingCount = Convert.ToInt32(sqlData["RatingCount"]);
                            bookModel.BookImage = sqlData["BookImage"].ToString();
                            bookModel.BookQuantity = Convert.ToInt32(sqlData["BookQuantity"]);
                            result.Add(bookModel);
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
