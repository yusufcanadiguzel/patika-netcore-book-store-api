namespace BookStore.WebApi.BookOperations.DeleteBook;

public class DeleteBookCommand{
    private readonly BookStoreDbContext _dbContext;

    public DeleteBookCommand(BookStoreDbContext dbContext, int bookId)
    {
        _dbContext = dbContext;
        BookId = bookId;
    }

    public int BookId { get; set; }

    public void Handle(){
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

        if(book is null)
            throw new InvalidOperationException("Silinecek kitap bulunamadı.");

        _dbContext.Books.Remove(book);

        _dbContext.SaveChanges();
    }
}