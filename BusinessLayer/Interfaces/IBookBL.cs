using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public string AddBook(BookModel bookModel);
       public string UpdateBookDetails(BookModel bookModel);
    }
}
