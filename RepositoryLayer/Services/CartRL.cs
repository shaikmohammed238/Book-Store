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
    public class CartRL:ICartRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;

        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddCart(CartModel cartModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_AddingCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", cartModel.ID);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Book Added succssfully to Cart";
                    
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string DeleteCart(int cartID)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_DeleteCartDetails", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartID",cartID);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "CartID does not exists";
                    }
                    else
                    {
                        return "Cart details deleted successfully";
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public IList<GetCartModel> GetCartDetails( )
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    List<GetCartModel> result = new List<GetCartModel>();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetCartDetails", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            BookModel bookModel = new BookModel();
                            GetCartModel getCartModel = new GetCartModel();

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
                            getCartModel.ID = Convert.ToInt32(sqlData["ID"]);
                            getCartModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            getCartModel.OrderQuantity = Convert.ToInt32(sqlData["OrderQuantity"]);
                            getCartModel.bookModel = bookModel;
                            result.Add(getCartModel);
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string UpdateCartQuantity(int cartID, int OrderQuantity)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_UpdateQuantityCart", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartID", cartID);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity",  OrderQuantity);
                    sqlConnection.Open();
                    int value = sqlCommand.ExecuteNonQuery();
                    if (value==1)
                    {
                        return "Successfully updated cart";
                    }
                    else
                    {
                        return "check this your cartId";
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
