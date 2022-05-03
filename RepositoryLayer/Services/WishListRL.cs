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
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddWishlist(WishListModel wishlist)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_CreateWishlist", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@UserId", wishlist.ID);
                    sqlcmd.Parameters.AddWithValue("@BookId", wishlist.BookId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Book already added to wishlist";
                    }
                    else
                    {
                        return "Book Wishlisted successfully";
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

        public IList<GetWishListModel> GetWishlist(int iD)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_ShowWishlistByUserId", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@UserId", iD);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlcmd.ExecuteReader();
                    List<GetWishListModel> wishlist = new List<GetWishListModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            GetWishListModel wish = new GetWishListModel();
                            BookWishModel bookModel = new BookWishModel();
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPriceValue = Convert.ToInt32(sqlData["DiscountPriceValue"]);
                            bookModel.OriginalPriceValue = Convert.ToInt32(sqlData["OriginalPriceValue"]);
                            bookModel.BookDescription = sqlData["BookDescription"].ToString();
                            bookModel.BookImage = sqlData["BookImage"].ToString();
                            wish.WishlistId = Convert.ToInt32(sqlData["WishListId"]);
                            wish.ID = Convert.ToInt32(sqlData["UserId"]);
                            wish.BookId = Convert.ToInt32(sqlData["BookId"]);
                            wish.Books = bookModel;
                            wishlist.Add(wish);
                        }
                        return wishlist;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public string RemoveBookFromWishlist(int wishlistId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_DeleteWishlist", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                    sqlConnection.Open();
                    sqlcmd.ExecuteNonQuery();
                    return "Wishlist deleted successfully";

                }

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
