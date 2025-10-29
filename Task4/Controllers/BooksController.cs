using Microsoft.AspNetCore.Mvc;
using Task4.Core.Abstractions;
using Task4.Domain.DTOs;

namespace Task4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> Get() => Ok(await _bookService.GetAllBooksAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Post(CreateBookDto book)
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
        public async Task<ActionResult<BookDto>> Put(int id, CreateBookDto book)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, book);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("after-year")]
        public async Task<ActionResult<List<BookDto>>> GetBooksAfterYear([FromQuery] int year)
        {
            return Ok(await _bookService.GetAllBooksAfterYearAsync(year));
        }
    }
}