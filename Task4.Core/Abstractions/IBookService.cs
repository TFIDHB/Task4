using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Domain.DTOs;
using Task4.Domain.Models;

namespace Task4.Core.Abstractions
{
    public interface IBookService
    {
        Task<BookDto> CreateBookAsync(CreateBookDto dto);
        Task<List<BookDto>> GetAllBooksAsync();
        Task<List<BookDto>> GetAllBooksAfterYearAsync(int year);
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto> UpdateBookAsync(int id, CreateBookDto dto);
        Task DeleteBookAsync(int id);
    }
}
