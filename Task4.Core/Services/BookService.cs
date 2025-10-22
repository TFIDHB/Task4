using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Core.Models;

namespace Task4.Core.Services
{
    public class BookService : IBookService
    {
        private List<Book> _books = new List<Book>();
        private readonly IAuthorService _authorService;
        private int _nextId = 1;


        public BookService(IAuthorService authorService) { 

            _authorService = authorService;
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Необходимо ввести название книги");

            var authorExists = await _authorService.GetAuthorByIdAsync(book.AuthorId);
            if (authorExists == null)
                throw new InvalidOperationException(Messages.authorNotFound);

            book.Id = _nextId++;
            _books.Add(book);
            return book;

        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return _books.ToList();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);

            var authorExists = await _authorService.GetAuthorByIdAsync(book.AuthorId);
            if (authorExists == null)
                throw new InvalidOperationException(Messages.authorNotFound);

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.PublishedYear = book.PublishedYear;
            }

            else
            {
                throw new KeyNotFoundException(Messages.bookNotFound);
            }

            return existingBook;

        }

        public async Task<Book> DeleteBookAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                throw new KeyNotFoundException(Messages.bookNotFound);

            _books.Remove(book);
            return book;

        }
    }
}
