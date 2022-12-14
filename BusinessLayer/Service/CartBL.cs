using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddToCart(int userId, int bookId, int cartQty)
        {
            try
            {
                return cartRL.AddToCart(userId, bookId, cartQty);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCart(int cartQty, int userId, int cartId)
        {
            try
            {
                return cartRL.UpdateCart(cartQty, userId, cartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteFromCart(int userId, int cartId)
        {
            try
            {
                return cartRL.DeleteFromCart(userId, cartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                return cartRL.GetCartDetails(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
