using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL:IWishListBL
    {
        IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public string AddWishlist(WishListModel wishlist)
        {
            try
            {
                return this.wishListRL.AddWishlist(wishlist);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string RemoveBookFromWishlist(int wishlistId)
        {
            try
            {
                return this.wishListRL.RemoveBookFromWishlist(wishlistId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<GetWishListModel> GetWishlist(int iD)
        {
            try
            {
                return this.wishListRL.GetWishlist(iD);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
