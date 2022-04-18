using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public string AddBook(BookModel bookModel);
        public string UpdateBookDetais(BookModel bookModel);
        public string DeleteBook(long bookId);
        public List<BookModel> GetBookByBookId(long bookId);
        public List<BookModel> GetAllBooks();
    }
}
