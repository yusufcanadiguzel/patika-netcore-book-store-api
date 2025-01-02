using FluentValidation;

namespace BookStore.WebApi.BookOperations.GetBookById;

public class GetBookByIdValidator : AbstractValidator<GetBookByIdQuery>{
    public GetBookByIdValidator()
    {
        RuleFor(x => x.BookId).GreaterThan(0);
    }
}