using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class BookWishModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public float DiscountPriceValue { get; set; }
        public float OriginalPriceValue { get; set; }
        public string BookDescription { get; set; }
        public string BookImage { get; set; }
    }
}
