using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        public string AddWishlist(WishListModel wishlist);
        public IList<GetWishListModel> GetWishlist(int iD);
        public string RemoveBookFromWishlist(int wishlistId);
    }
}
