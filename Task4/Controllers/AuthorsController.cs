using Microsoft.AspNetCore.Mvc;
using Task4.Core.Abstractions;
using Task4.Domain.DTOs;

namespace Task4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> Get() => Ok(await _authorService.GetAllAuthorsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Post(CreateAuthorDto author)
        {
            try
            {
                var createdAuthor = await _authorService.CreateAuthorAsync(author);
                return CreatedAtAction(nameof(Get), new { id = createdAuthor.Id }, createdAuthor);
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
        public async Task<ActionResult<AuthorDto>> Put(int id, CreateAuthorDto author)
        {
            try
            {
                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author);
                return Ok(updatedAuthor);
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
                await _authorService.DeleteAuthorAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("with-book-count")]
        public async Task<ActionResult<List<AuthorWithBookCountDto>>> GetAuthorsWithBookCount()
        {
            return Ok(await _authorService.GetAllAuthorsWithBookCountAsync());
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<AuthorDto>>> Search(string name)
        {
            return Ok(await _authorService.SearchAuthorsByNameAsync(name));
        }
    }
}