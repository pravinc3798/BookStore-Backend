using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                var result = userBL.AddUser(userModel);
                if (result != null)
                    return Ok(new { success = true, message = "User has been registered", data = result });
                else
                    return BadRequest(new { success = false, message = "Registration Unsuccessfull" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string emailId, string userPassword)
        {
            try
            {
                var result = userBL.Login(emailId, userPassword);
                if (result != null)
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                else
                    return BadRequest(new { success = false, message = "Login Unsuccessfull" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBL.ForgetPassword(email);

                if (result != null)
                    return Ok(new { success = true, message = "email sent successfully", data = result });
                else
                    return BadRequest(new { success = false, message = "email not sent" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(Email, password, confirmPassword);

                if (result != false)
                    return Ok(new { success = true, message = "password has been reset" });
                else
                    return BadRequest(new { success = false, message = "try again" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
