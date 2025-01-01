using BookStore.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi;

public static class DataGenarator
{
    public static void Initialize(this BookStoreDbContext dbContext)
    {
        if (dbContext.Books.Any())
                return;

            dbContext.Books.AddRange(
                new Book
                {
                    Title = "1984",
                    GenreId = 1, // Bilim Kurgu
                    PageCount = 200,
                    PublishDate = new DateTime(2000, 1, 1)
                },
                new Book
                {
                    Title = "İlahi Komedya",
                    GenreId = 2, // Klasikler
                    PageCount = 300,
                    PublishDate = new DateTime(2000, 1, 1)
                },
                new Book
                {
                    Title = "Atomik Alışkanlıklar",
                    GenreId = 3, // Kişisel Gelişim
                    PageCount = 400,
                    PublishDate = new DateTime(2000, 1, 1)
                }
            );

            dbContext.SaveChanges();
    }
}