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
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Необходимо ввести название книги");

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                throw new InvalidOperationException(Messages.authorNotFound);

            var newBook = new Book
            {
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return new BookDto
            {
                Id = newBook.Id,
                Title = newBook.Title,
                PublishedYear = newBook.PublishedYear,
                AuthorId = newBook.AuthorId
            };
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                PublishedYear = b.PublishedYear,
                AuthorId = b.AuthorId
            }).ToList();
        }

        public async Task<List<BookDto>> GetAllBooksAfterYearAsync(int year)
        {
            var books = await _context.Books
                .Where(b => b.PublishedYear > year)
                .ToListAsync();

            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                PublishedYear = b.PublishedYear,
                AuthorId = b.AuthorId
            }).ToList();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            };
        }

        public async Task<BookDto> UpdateBookAsync(int id, CreateBookDto book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                throw new InvalidOperationException(Messages.authorNotFound);

            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (existingBook == null)
                throw new KeyNotFoundException(Messages.bookNotFound);

            existingBook.Title = book.Title;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.AuthorId = book.AuthorId;

            await _context.SaveChangesAsync();

            return new BookDto
            {
                Id = existingBook.Id,
                Title = existingBook.Title,
                PublishedYear = existingBook.PublishedYear,
                AuthorId = existingBook.AuthorId
            };
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                throw new KeyNotFoundException(Messages.bookNotFound);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}