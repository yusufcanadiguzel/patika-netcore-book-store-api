using AutoMapper;
using BookStore.WebApi.BookOperations.GetBooks;
using BookStore.WebApi.Common;

namespace BookStore.WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookByIdQuery(BookStoreDbContext dbContext, int bookId, IMapper mapper)
    {
        _dbContext = dbContext;
        BookId = bookId;
        _mapper = mapper;
    }

    public int BookId { get; set; }

    public BookDetailsViewModel Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

        if(book is null)
            throw new InvalidOperationException("Kitap sistemde bulunamadı.");

        var bookViewModel = _mapper.Map<BookDetailsViewModel>(book);
        
        return bookViewModel;
    }
}

public class BookDetailsViewModel {
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
}