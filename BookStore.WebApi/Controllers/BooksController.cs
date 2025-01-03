using AutoMapper;
using BookStore.WebApi.BookOperations.CreateBook;
using BookStore.WebApi.BookOperations.DeleteBook;
using BookStore.WebApi.BookOperations.GetBookById;
using BookStore.WebApi.BookOperations.GetBooks;
using BookStore.WebApi.BookOperations.UpdateBook;
using FluentValidation;
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
        var getBookByIdQuery = new GetBookByIdQuery(_context, id, _mapper);

        try
        {
            var validator = new GetBookByIdValidator();

            validator.ValidateAndThrow(getBookByIdQuery);

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
        var createBookCommand = new CreateBookCommand(_context, _mapper);

        createBookCommand.BookModel = addedBook;

        var validator = new CreateBookValidator();

        validator.ValidateAndThrow(createBookCommand);

        createBookCommand.Handle();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] UpdateBookViewModel updatedBook)
    {
        var updateBookCommand = new UpdateBookCommand(_context, id);

        try
        {
            updateBookCommand.BookModel = updatedBook;

            var validator = new UpdateBookValidator();

            validator.ValidateAndThrow(updateBookCommand);

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

            var validator = new DeleteBookValidator();

            validator.ValidateAndThrow(deleteBookCommand);

            deleteBookCommand.Handle();

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}