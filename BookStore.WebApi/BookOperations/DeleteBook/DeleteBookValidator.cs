using FluentValidation;

namespace BookStore.WebApi.BookOperations.DeleteBook;

public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>{
    public DeleteBookValidator()
    {
        RuleFor(x => x.BookId).GreaterThan(0);
    }
}