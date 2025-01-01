using BookStore.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi;

public class DataGenarator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any())
                return;

            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    GenreId = 1, // Bilim Kurgu
                    PageCount = 200,
                    PublishDate = new DateTime(2000, 1, 1)
                },
                new Book
                {
                    Id = 2,
                    Title = "İlahi Komedya",
                    GenreId = 2, // Klasikler
                    PageCount = 300,
                    PublishDate = new DateTime(2000, 1, 1)
                },
                new Book
                {
                    Id = 3,
                    Title = "Atomik Alışkanlıklar",
                    GenreId = 3, // Kişisel Gelişim
                    PageCount = 400,
                    PublishDate = new DateTime(2000, 1, 1)
                }
            );

            context.SaveChanges();
        }
    }
}