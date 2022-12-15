using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddAddress(AddressModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.AddAddress(userId, model);

                if(result)
                    return Ok(new { success = true, message = "Address Added" });
                else
                    return BadRequest(new { success = false, message = "Address not added !!" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateAddress(AddressModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.UpdateAddress(userId, model);

                if (result)
                    return Ok(new { success = true, message = "Address Updated" });
                else
                    return BadRequest(new { success = false, message = "Address not updated !!" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("ViewAll")]
        public IActionResult ViewAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.GetAllAddresses(userId);

                if (result != null)
                    return Ok(new { success = true, data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong !!" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.RemoveAddress(userId, addressId);

                if (result)
                    return Ok(new { success = true, message = "Address deleted" });
                else
                    return BadRequest(new { success = false, message = "Address not deleted !!" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
