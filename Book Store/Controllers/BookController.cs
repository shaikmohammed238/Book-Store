using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
       
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost]
        [Route("AddBooks")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                string result = this.bookBL.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Book Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Book Added UnSuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPut]
        [Route("UpdateBook/{BookId}")]
        public IActionResult UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                string result = this.bookBL.UpdateBookDetails(bookModel);
                if (result.Equals(true))
                {
                    return this.BadRequest(new { Status = false, Message = $"Book not updated" });
                }
                else
                {
                    return this.Ok(new { success = true, message = $"Book Update Successfully" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteBook/{BookId}")]
        public IActionResult DeleteBook(long BookId)
        {
            try
            {
                string result = this.bookBL.DeleteBook(BookId);
                if (result.Equals(true))
                {
                    return this.BadRequest(new { Status = false, Message = $"Book is unable to delete check book id" });
                }
                else
                {
                    return this.Ok(new { success = true, message = $"Book deleted Successfully from bookstore" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("GetBook/{BookId}")]
        public IActionResult GetBookByBookId(long BookId)
        {
            try
            {
                var result = this.bookBL.GetBookByBookId(BookId);
                if (result.Equals(true))
                {
                    return this.BadRequest(new { Status = false, Message = $"Book is unable to get check book id" });
                }
                else
                {
                    return this.Ok(new { success = true, message = $"Book get Successfully from bookstore",data=result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();
                if (result.Equals(true))
                {
                    return this.BadRequest(new { Status = false, Message = $"check userid " });
                }
                else
                {
                    return this.Ok(new { success = true, message = $"Get All books data", data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
