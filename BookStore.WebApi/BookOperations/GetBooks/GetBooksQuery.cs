using BookStore.WebApi.Common;

namespace BookStore.WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;

    public GetBooksQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<BookViewModel> Handle()
    {
        var books = _dbContext.Books.ToList();
        var booksViewModel = new List<BookViewModel>();

        foreach (var book in books)
        {
            booksViewModel.Add(new BookViewModel
            {
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.ToString("dd/MM/yyyy")
            });
        }

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