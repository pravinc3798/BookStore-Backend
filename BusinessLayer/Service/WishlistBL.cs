using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;

        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public bool AddToWishlist(int userId, int bookId)
        {
            try
            {
                return wishlistRL.AddToWishlist(userId, bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WishlistModel> ViewWishlist(int userId)
        {
            try
            {
                return wishlistRL.ViewWishlist(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                return wishlistRL.DeleteFromWishlist(userId, wishlistId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
