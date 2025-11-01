using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4.Core.Abstractions;
using Task4.Domain.Models;
using Task4.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Task4.Domain.DTOs;

namespace Task4.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;
        public AuthorService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException(Messages.nameRequired);

            var newAuthor = new Author
            {
                Name = author.Name,
                DateOfBirth = author.DateOfBirth
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return new AuthorDto
            {
                Id = newAuthor.Id,
                Name = newAuthor.Name,
                DateOfBirth = newAuthor.DateOfBirth
            };
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth
            }).ToList();
        }

        public async Task<List<AuthorWithBookCountDto>> GetAllAuthorsWithBookCountAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorWithBookCountDto
                {
                    Name = a.Name,
                    BookCount = a.Books.Count
                })
                .ToListAsync();
        }

        public async Task<List<AuthorDto>> SearchAuthorsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<AuthorDto>();

            var authors = await _context.Authors
                .Where(a => a.Name.Contains(name))
                .ToListAsync();

            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth
            }).ToList();
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth
            };
        }

        public async Task<AuthorDto> UpdateAuthorAsync(int id, CreateAuthorDto author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (existingAuthor == null)
                throw new KeyNotFoundException(Messages.authorNotFound);

            existingAuthor.Name = author.Name;
            existingAuthor.DateOfBirth = author.DateOfBirth;

            await _context.SaveChangesAsync();

            return new AuthorDto
            {
                Id = existingAuthor.Id,
                Name = existingAuthor.Name,
                DateOfBirth = existingAuthor.DateOfBirth
            };
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                throw new KeyNotFoundException(Messages.authorNotFound);

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}