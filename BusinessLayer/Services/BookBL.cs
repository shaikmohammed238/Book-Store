using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL:IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public string AddBook(BookModel bookModel)
        {
            try
            {
                return bookRL.AddBook(bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteBook(long bookId)
        {
            try
            {
                return bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BookModel> GetBookByBookId(long bookId)
        {
            try
            {
                return bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                return bookRL.UpdateBookDetais(bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
