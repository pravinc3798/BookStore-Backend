using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult AddAddAdministrator(string administrator, string adminPassword)
        {
            try
            {
                var result = adminBL.AddAdministrator(administrator, adminPassword);
                if (result)
                    return Ok(new { success = true, message = "Admin has been registered"});
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
        public IActionResult Login(string administrator, string adminPassword)
        {
            try
            {
                var result = adminBL.Login(administrator, adminPassword);
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
    }
}
