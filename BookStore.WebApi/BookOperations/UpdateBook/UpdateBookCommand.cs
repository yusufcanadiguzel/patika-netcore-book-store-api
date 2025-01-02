namespace BookStore.WebApi.BookOperations.UpdateBook;

public class UpdateBookCommand{
    private readonly BookStoreDbContext _dbContext;

    public UpdateBookCommand(BookStoreDbContext dbContext, int id)
    {
        _dbContext = dbContext;
        BookId = id;
    }

    public UpdateBookViewModel BookModel { get; set; }
    public int BookId { get; set; }

    public void Handle(){
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

        if (book is null)
            throw new Exception("Kitap sistemde bulunamadı.");

        book.Title = (BookModel.Title != default) ? BookModel.Title : book.Title;
        book.PageCount = (BookModel.PageCount != default) ? BookModel.PageCount : book.PageCount;
        book.PublishDate = (BookModel.PublishDate != default) ? BookModel.PublishDate : book.PublishDate;
        book.GenreId = (BookModel.GenreId != default) ? BookModel.GenreId : book.GenreId;

        _dbContext.SaveChanges();
    }
}

public class UpdateBookViewModel{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public int GenreId { get; set; }
    public DateTime PublishDate { get; set; }
}