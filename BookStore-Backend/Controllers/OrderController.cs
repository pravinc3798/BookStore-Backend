using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System;
using CommonLayer.Model;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            var result = orderBL.AddOrder(orderModel);

            if (result != null)
                return Ok(new { success = true, message = "Order Successfull", data = result });
            else
                return BadRequest(new { success = false, message = "Order Unsuccessfull !!" });
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("View")]
        public IActionResult ViewAllOrders()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = orderBL.ViewOrders(userId);

            if (result != null)
                return Ok(new { success = true, data = result });
            else
                return BadRequest(new { success = false, message = "Something went wrong !!" });
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult CancelOrder(int orderId)
        {
            var result = orderBL.CancelOrder(orderId);

            if (result)
                return Ok(new { success = true, message = "Order Cancelled !!" });
            else
                return BadRequest(new { success = false, message = "Something went wrong !!" });
        }
    }
}
