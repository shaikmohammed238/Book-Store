using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class UserModel
    {
       
        [Required]
        [RegularExpression("^[A-Z][A-Z a-z]{2,}$",ErrorMessage = "Please enter a valid name ")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
        ErrorMessage = "Please enter correct Email Address")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression("^[A-Z][A-Z a-z 0-9]{2,}$", ErrorMessage = "Please enter a valid Password ")]
        public string Password { get; set; }
        //[Required]
        //[RegularExpression("^[91]?[6-9][0-9]{9}$", ErrorMessage = "Please enter a valid pattern number ")]
        public int PhoneNumber { get; set; }
    }
}
