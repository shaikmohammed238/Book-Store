using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public string AddCart(CartModel cartModel)
        {
            try
            {
                return cartRL.AddCart(cartModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteCart(int cartID)
        {
            try
            {
                return cartRL.DeleteCart(cartID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<GetCartModel> GetCartDetails()
        {
            try
            {
                return cartRL.GetCartDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateCartQuantity(int cartID, int OrderQuantity)
        {
            try
            {
                return cartRL.UpdateCartQuantity(cartID,  OrderQuantity);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
