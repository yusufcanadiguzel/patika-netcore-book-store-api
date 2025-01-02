using BookStore.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> dbContextOptions) : base(dbContextOptions) 
    {
        
    }

    public DbSet<Book> Books { get; set; }
}