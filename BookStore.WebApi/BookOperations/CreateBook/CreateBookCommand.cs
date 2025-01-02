using BookStore.WebApi.Controllers;

namespace BookStore.WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CreateBookViewModel BookModel { get; set; }

    public void Handle(){
        var book = _dbContext.Books.SingleOrDefault(b => b.Title == BookModel.Title);

        if (book is not null)
            throw new Exception("Kitap zaten sistemde mevcut.");

        book = new Book();
        
        book.Title = BookModel.Title;
        book.GenreId = BookModel.GenreId;
        book.PageCount = BookModel.PageCount;
        book.PublishDate = BookModel.PublishDate;

        _dbContext.Books.Add(book);

        _dbContext.SaveChanges();
    }
}

public class CreateBookViewModel{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public int GenreId { get; set; }
}