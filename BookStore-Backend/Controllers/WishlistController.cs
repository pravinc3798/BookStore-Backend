using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;

        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddToWishlist(int bookId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result = wishlistBL.AddToWishlist(userId, bookId);

            if (result)
                return Ok(new { success = true, message = "Book added to wishlist" });
            else
                return BadRequest(new { success = false, message = "Book not added !!" });
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("View")]
        public IActionResult ViewWishlist()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result = wishlistBL.ViewWishlist(userId);

            if (result != null)
                return Ok(new { success = true, data = result });
            else
                return BadRequest(new { success = false, message = "Something went wrong !!" });
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteFromWishlist(int wishlistId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result = wishlistBL.DeleteFromWishlist(userId, wishlistId);

            if (result)
                return Ok(new { success = true, message = "Book removed from wishlist" });
            else
                return BadRequest(new { success = false, message = "Something went wrong !!" });
        }
    }
}
