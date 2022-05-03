using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class BookGetOrderModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int DiscountPriceValue { get; set; }
        public int OriginalPriceValue { get; set; }
        public string BookDescription { get; set; }
        public string BookImage { get; set; }
    }
}
