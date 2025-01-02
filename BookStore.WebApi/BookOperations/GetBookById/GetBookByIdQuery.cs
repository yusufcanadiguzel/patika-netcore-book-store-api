using BookStore.WebApi.BookOperations.GetBooks;
using BookStore.WebApi.Common;

namespace BookStore.WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    private readonly BookStoreDbContext _dbContext;

    public GetBookByIdQuery(BookStoreDbContext dbContext, int bookId)
    {
        _dbContext = dbContext;
        BookId = bookId;
    }

    public int BookId { get; set; }

    public BookViewModel Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

        if(book is null)
            throw new Exception("Kitap sistemde bulunamadı.");

        var bookViewModel = new BookViewModel{
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.ToString("dd/MM/yyyy")
        };

        return bookViewModel;
    }
}