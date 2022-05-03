using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public string AddWishlist(WishListModel wishlist);
        public string RemoveBookFromWishlist(int wishlistId);
        public IList<GetWishListModel> GetWishlist(int iD);
    }
}
