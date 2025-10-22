using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Core.Models;

namespace Task4.Core.Services
{
    public interface IAuthorService
    {
        Task<Author> CreateAuthorAsync(Author author);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> UpdateAuthorAsync(Author author);
        Task<Author> DeleteAuthorAsync(int id);
    }
}
