using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> _books = new List<Book>{
        new Book {
            Id = 1,
            Title = "1984",
            GenreId = 1, // Bilim Kurgu
            PageCount = 200,
            PublishDate = new DateTime(2000,1,1)
        },
        new Book {
            Id = 2,
            Title = "İlahi Komedya",
            GenreId = 2, // Klasikler
            PageCount = 300,
            PublishDate = new DateTime(2000,1,1)
        },
        new Book {
            Id = 3,
            Title = "Atomik Alışkanlıklar",
            GenreId = 3, // Kişisel Gelişim
            PageCount = 400,
            PublishDate = new DateTime(2000,1,1)
        }
    };

    [HttpGet]
    public List<Book> GetAllBooks(){
        var books = _books.OrderBy(b => b.Id).ToList();

        return books;
    }

    [HttpGet("{id:int}")]
    public Book GetOneBookById([FromRoute(Name = "id")] int id){
        var book = _books.Where(b => b.Id == id).SingleOrDefault();

        return book;
    }

    // [HttpGet]
    // public Book GetOneBookById([FromQuery] string id){
    //     var book = _books.Where(b => b.Id == int.Parse(id)).SingleOrDefault();

    //     return book;
    // }
}