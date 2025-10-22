using Microsoft.AspNetCore.Mvc;
using Task4.Core.Models;
using Task4.Core.Services;

namespace Task4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController: ControllerBase
    {

        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {

            _bookService = bookService;

        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get() => Ok(await _bookService.GetAllBooksAsync());

        [HttpGet("{id}")]

        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]

        public async Task<ActionResult<Book>> Post(Book book)
        {
            try
            {
                var createdBook = await _bookService.CreateBookAsync(book);
                return CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Book>> Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(book);
                return Ok(updatedBook);

            }


            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Book>> Delete(int id)
        {
            try
            {
                var deletedBook = await _bookService.DeleteBookAsync(id);
                return Ok(deletedBook);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
