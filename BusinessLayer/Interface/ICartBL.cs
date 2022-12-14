using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public bool AddToCart(int userId, int bookId, int cartQty);
        public bool UpdateCart(int cartQty, int userId, int cartId);
        public bool DeleteFromCart(int userId, int cartId);
        public List<CartModel> GetCartDetails(int userId);
    }
}
