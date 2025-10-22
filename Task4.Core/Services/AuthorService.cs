using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Core.Models;

namespace Task4.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private List<Author> _authors = new List<Author>();
        private int _nextId = 1;

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("Необходимо ввести имя автора");

            author.Id = _nextId++;
            _authors.Add(author);
            return author;

        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return _authors.ToList();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return _authors.FirstOrDefault(a => a.Id == id);
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);

            if (existingAuthor != null) { 
                existingAuthor.Name = author.Name;
                existingAuthor.DateOfBirth = author.DateOfBirth;
            }

            else
            {
                throw new KeyNotFoundException(Messages.authorNotFound);
            }

            return existingAuthor;

        }

        public async Task<Author> DeleteAuthorAsync(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                throw new KeyNotFoundException(Messages.authorNotFound);

            _authors.Remove(author);
            return author;

        }
    }
}
