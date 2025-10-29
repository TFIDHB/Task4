namespace Task4.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
