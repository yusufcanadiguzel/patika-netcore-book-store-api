using AutoMapper;
using BookStore.WebApi.BookOperations.CreateBook;
using BookStore.WebApi.BookOperations.GetBookById;
using BookStore.WebApi.BookOperations.GetBooks;
using BookStore.WebApi.Controllers;

namespace BookStore.WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookViewModel, Book>();
        CreateMap<Book, BookDetailsViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src =>((GenreEnum)src.GenreId).ToString()));
        CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src =>((GenreEnum)src.GenreId).ToString()));
    }
}