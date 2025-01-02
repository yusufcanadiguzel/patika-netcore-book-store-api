using System.Data;
using FluentValidation;

namespace BookStore.WebApi.BookOperations.CreateBook;

public class CreateBookValidator : AbstractValidator<CreateBookCommand>{
    public CreateBookValidator()
    {
        RuleFor(x => x.BookModel.Title).NotEmpty();
        RuleFor(x => x.BookModel.PageCount).GreaterThan(0);
        RuleFor(x => x.BookModel.GenreId).GreaterThan(0);
        RuleFor(x => x.BookModel.PublishDate.Date).LessThan(DateTime.Now.Date);
    }
}