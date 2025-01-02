using AutoMapper;
using BookStore.WebApi.BookOperations.CreateBook;
using BookStore.WebApi.BookOperations.DeleteBook;
using BookStore.WebApi.BookOperations.GetBookById;
using BookStore.WebApi.BookOperations.GetBooks;
using BookStore.WebApi.BookOperations.UpdateBook;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BooksController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var getBooksQuery = new GetBooksQuery(_context, _mapper);

        var books = getBooksQuery.Handle();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOneBookById([FromRoute(Name = "id")] int id)
    {
        try
        {
            var getBookByIdQuery = new GetBookByIdQuery(_context, id, _mapper);

            var bookViewModel = getBookByIdQuery.Handle();

            return Ok(bookViewModel);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    // [HttpGet]
    // public Book GetOneBookById([FromQuery] string id){
    //     var book = _books.Where(b => b.Id == int.Parse(id)).SingleOrDefault();

    //     return book;
    // }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] CreateBookViewModel addedBook)
    {
        try
        {
            var createBookCommand = new CreateBookCommand(_context, _mapper);

            createBookCommand.BookModel = addedBook;

            createBookCommand.Handle();

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] UpdateBookViewModel updatedBook)
    {
        try
        {
            var updateBookCommand = new UpdateBookCommand(_context, id);

            updateBookCommand.BookModel = updatedBook;

            updateBookCommand.Handle();

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        try
        {
            var deleteBookCommand = new DeleteBookCommand(_context, id);

            deleteBookCommand.Handle();
            
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}