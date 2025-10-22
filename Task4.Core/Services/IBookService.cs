using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Core.Models;

namespace Task4.Core.Services
{
    public interface IBookService
    {
        Task<Book> CreateBookAsync(Book book);
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> UpdateBookAsync(Book book);
        Task<Book> DeleteBookAsync(int id);
    }
}
