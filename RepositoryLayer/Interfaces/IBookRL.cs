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
    }
}
