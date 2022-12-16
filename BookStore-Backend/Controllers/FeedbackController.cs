using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("Write")]
        public IActionResult WriteFeedback(int bookId, FeedbackModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = feedbackBL.AddFeedback(userId, bookId, model);

                if (result != null)
                    return Ok(new { success = true, message = "Feedback added", data = result });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("MyFeedbacks")]
        public IActionResult GetFeedbacks()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = feedbackBL.MyFeedbacks(userId);

                if (result != null)
                    return Ok(new { success = true, data = result });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = feedbackBL.DeleteFeedback(feedbackId, userId);

                if (result)
                    return Ok(new { success = true, message = "Feedback Deleted" });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
