using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public List<Book> GetAllBooks()
    {
        var books = _books.OrderBy(b => b.Id).ToList();

        return books;
    }

    [HttpGet("{id:int}")]
    public Book GetOneBookById([FromRoute(Name = "id")] int id)
    {
        var book = _books.Where(b => b.Id == id).SingleOrDefault();

        return book;
    }

    // [HttpGet]
    // public Book GetOneBookById([FromQuery] string id){
    //     var book = _books.Where(b => b.Id == int.Parse(id)).SingleOrDefault();

    //     return book;
    // }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] Book addedBook)
    {
        if (_books.SingleOrDefault(b => b.Id == addedBook.Id) is not null)
            return BadRequest();

        _books.Add(addedBook);

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book updatedBook)
    {
        var bookEntity = _books.SingleOrDefault(b => b.Id == id);

        if (bookEntity is null)
            return BadRequest();

        bookEntity.Title = (updatedBook.Title == default) ? bookEntity.Title : updatedBook.Title;
        bookEntity.GenreId = (updatedBook.GenreId == default) ? bookEntity.GenreId : updatedBook.GenreId;
        bookEntity.PageCount = (updatedBook.PageCount == default) ? bookEntity.PageCount : updatedBook.PageCount;
        bookEntity.PublishDate = (updatedBook.PublishDate == default) ? bookEntity.PublishDate : updatedBook.PublishDate;

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        var bookEntity = _books.SingleOrDefault(b => b.Id == id);

        if (bookEntity is null)
            return BadRequest();

        _books.Remove(bookEntity);

        return Ok();
    }
}