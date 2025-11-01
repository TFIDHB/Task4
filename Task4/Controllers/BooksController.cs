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
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Post(CreateBookDto book)
        {
            var createdBook = await _bookService.CreateBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> Put(int id, CreateBookDto book)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpGet("after-year")]
        public async Task<ActionResult<List<BookDto>>> GetBooksAfterYear([FromQuery] int year)
        {
            return Ok(await _bookService.GetAllBooksAfterYearAsync(year));
        }
    }
}