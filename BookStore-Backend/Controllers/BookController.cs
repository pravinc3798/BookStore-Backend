using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookBL.AddBook(bookModel);

                if (result != null)
                    return Ok(new {success = true, message = "Book Added !!", data = result});
                else
                    return BadRequest(new {success = false, message = "Book not added !?"});
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("Edit")]
        public IActionResult EditBook(BookModel bookModel)
        {
            try
            {
                var result = bookBL.EditBook(bookModel);

                if (result != null)
                    return Ok(new { success = true, message = "Book Updated !!", data = result });
                else
                    return BadRequest(new { success = false, message = "Book not Updated !?" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("AllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();

                if (result != null)
                    return Ok(new { success = true, message = "All Books !!", data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong !?" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetABook")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var result = bookBL.GetBookById(bookId);

                if (result != null)
                    return Ok(new { success = true,data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong !?" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = bookBL.DeleteBook(bookId);

                if (result)
                    return Ok(new { success = true, message = "Book Deleted!!"});
                else
                    return BadRequest(new { success = false, message = "Something went wrong !?" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
