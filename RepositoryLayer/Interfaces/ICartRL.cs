using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public string AddCart(CartModel cartModel);
        public string DeleteCart(int cartID);
        public IList<GetCartModel> GetCartDetails();
        public string UpdateCartQuantity(int cartID, int OrderQuantity);
    }
}
