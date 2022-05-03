using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Experimental.System.Messaging;
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
    public class CartController : Controller
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [Authorize]
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddCart(CartModel cartModel)
        {
            try
            {
                string Result = this.cartBL.AddCart(cartModel);
                if (Result != null)
                {
                    return this.Ok(new {success=true,message=$"Added book in cart"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"book not added in cart" });
                }

            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteBook/{CartID}")]
        public IActionResult DeleteCart(int CartID)
        {
            try
            {
                string result = this.cartBL.DeleteCart(CartID);
                if (result.Equals("Cart details deleted successfully"))
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

        [Authorize]
        [HttpGet]
        [Route("GetCartDetails/{UserId}")]
        public IActionResult GetCartDetails( )
        {
            try
            {
                var result = this.cartBL.GetCartDetails( );
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Data retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Get cart details is unsuccessful" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateCart/{CartID}/")]
        public IActionResult UpdateCartQuantity(int CartID, int OrderQuantity)
        {
            try
            {
                string result = cartBL.UpdateCartQuantity(CartID, OrderQuantity);
                if (result!=null)
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
    }

}
