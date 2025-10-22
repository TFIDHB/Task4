using Microsoft.AspNetCore.Mvc;
using Task4.Core.Services;
using Task4.Core.Models;

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
        public async Task<ActionResult<List<Author>>> Get() => Ok(await _authorService.GetAllAuthorsAsync());

        [HttpGet("{id}")]

        public async Task<ActionResult<Author>> Get(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]

        public async Task<ActionResult<Author>> Post(Author author)
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

        public async Task<ActionResult<Author>> Put(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedAuthor = await _authorService.UpdateAuthorAsync(author);
                return Ok(updatedAuthor);

            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (KeyNotFoundException ex) { 
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Author>> Delete(int id)
        {
            try
            {
                var deletedAuthor = await _authorService.DeleteAuthorAsync(id);
                return Ok(deletedAuthor);
            }

            catch (KeyNotFoundException ex) { 
                return NotFound(ex.Message);
            }
        }
    }
}
