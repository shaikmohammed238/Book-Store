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
