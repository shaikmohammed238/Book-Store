using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
       public string AddCart(CartModel cartModel);
       public string DeleteCart(int cartID);
       public string UpdateCartQuantity(int cartID, int OrderQuantity);
        public IList <GetCartModel> GetCartDetails();
    }
}
