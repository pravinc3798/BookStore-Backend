using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize (Roles = "User")]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddToCart(int bookId, int cartQty)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            
            var result = cartBL.AddToCart(userId, bookId, cartQty);

            if (result)
                return Ok(new { success = true, message = "Book added to cart" });
            else
                return BadRequest(new { success = false, message = "Book not added !!" });
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateCart(int cartId, int cartQty)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result = cartBL.UpdateCart(cartQty, userId, cartId );

            if (result)
                return Ok(new { success = true, message = "Cart Updated" });
            else
                return BadRequest(new { success = false, message = "something went wrong !!" });
        }


        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteFromCart(int cartId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result = cartBL.DeleteFromCart(userId, cartId);

            if (result)
                return Ok(new { success = true, message = "Book Removed" });
            else
                return BadRequest(new { success = false, message = "something went wrong !!" });
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("View")]
        public IActionResult ViewCart()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var result = cartBL.GetCartDetails(userId);

            if (result != null)
                return Ok(new { success = true, message = "Cart : ", data = result });
            else
                return BadRequest(new { success = false, message = "something went wrong !!" });
        }
    }
}
