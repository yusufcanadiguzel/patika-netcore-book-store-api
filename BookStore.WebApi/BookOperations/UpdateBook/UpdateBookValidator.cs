using FluentValidation;

namespace BookStore.WebApi.BookOperations.UpdateBook;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>{
    public UpdateBookValidator()
    {
        RuleFor(x => x.BookModel.PageCount).GreaterThan(-1);
        RuleFor(x => x.BookModel.GenreId).GreaterThan(-1);
        RuleFor(x => x.BookModel.PublishDate.Date).LessThan(DateTime.Now.Date);
    }
}