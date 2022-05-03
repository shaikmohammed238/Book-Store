using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class CartModel
    {
        public int ID { get; set; }
        [Required]
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
    }
}
