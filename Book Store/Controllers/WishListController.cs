using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
        IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }
        [HttpPost]
        [Route("AddToWishlist")]
        public IActionResult AddWishlist(WishListModel wishlist)
        {
            try
            {
                string result = this.wishListBL.AddWishlist(wishlist);
                if (result.Equals("Book Wishlisted successfully"))
                {

                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteWishlist/{WishlistId}")]
        public IActionResult RemoveBookFromWishlist(int WishlistId)
        {
            try
            {
                string result = this.wishListBL.RemoveBookFromWishlist(WishlistId);
                if (result.Equals("Wishlist deleted successfully"))
                {

                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("GetWishlistDetails/{ID}")]
        public IActionResult GetWishlist(int ID)
        {
            try
            {
                var result = this.wishListBL.GetWishlist(ID);
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "Wishlist Displayed", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to Read WishList" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }

}

