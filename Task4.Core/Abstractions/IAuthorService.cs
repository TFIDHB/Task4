using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Domain.DTOs;
using Task4.Domain.Models;

namespace Task4.Core.Abstractions
{
    public interface IAuthorService
    {
        Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto dto);
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        Task<List<AuthorWithBookCountDto>> GetAllAuthorsWithBookCountAsync();
        Task<List<AuthorDto>> SearchAuthorsByNameAsync(string name);
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> UpdateAuthorAsync(int id, CreateAuthorDto dto);
        Task DeleteAuthorAsync(int id);
    }
}
