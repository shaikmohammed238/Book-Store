using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class GetCartModel
    {
        public int ID { get; set; }
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
        public BookModel bookModel { get; set; }
    }

}
