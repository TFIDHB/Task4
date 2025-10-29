using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}
