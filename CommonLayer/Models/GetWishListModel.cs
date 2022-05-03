using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class GetWishListModel
    {
        public int WishlistId { get; set; }
        public int ID { get; set; }
        public int BookId { get; set; }
        public BookWishModel Books { get; set; }
    }
}
