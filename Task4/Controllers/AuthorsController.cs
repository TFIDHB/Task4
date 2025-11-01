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
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Post(CreateAuthorDto author)
        {
            var createdAuthor = await _authorService.CreateAuthorAsync(author);
            return CreatedAtAction(nameof(Get), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> Put(int id, CreateAuthorDto author)
        {
            var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author);
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
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