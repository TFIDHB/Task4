using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Domain.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
    }
}
