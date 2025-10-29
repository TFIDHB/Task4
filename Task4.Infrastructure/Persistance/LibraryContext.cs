using Microsoft.EntityFrameworkCore;
using Task4.Domain.Models;

namespace Task4.Infrastructure.Persistance
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public LibraryContext()
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=library.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(b => b.Books)
                .WithOne(a => a.Author)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Author>().HasData(new Author { Id = 1, Name = "Стивен Кинг", DateOfBirth = new DateTime(1947, 9, 21) });
            modelBuilder.Entity<Book>().HasData(new Book { Id = 1, Title = "Сияние", PublishedYear = 1977, AuthorId = 1 });
            modelBuilder.Entity<Book>().HasData(new Book { Id = 2, Title = "Кладбище домашних животных", PublishedYear = 1983, AuthorId = 1 });
        }
    }
}