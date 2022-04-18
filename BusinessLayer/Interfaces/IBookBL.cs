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
        public string DeleteBook(long bookId);
        public List<BookModel> GetBookByBookId(long bookId);
        public List<BookModel> GetAllBooks();
    }
}
