using AutoMapper;
using BookStore.WebApi.Common;

namespace BookStore.WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BookViewModel> Handle()
    {
        var books = _dbContext.Books.ToList();
        var booksViewModel = _mapper.Map<List<BookViewModel>>(books);

        return booksViewModel;
    }
}

public class BookViewModel
    {
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
    }