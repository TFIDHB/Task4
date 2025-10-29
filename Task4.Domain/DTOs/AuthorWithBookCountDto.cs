using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Domain.DTOs
{
    public class AuthorWithBookCountDto
    {
        public string Name { get; set; } = string.Empty;
        public int BookCount { get; set; }
    }
}
