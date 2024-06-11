using AutoMapper;
using BookStore.Authors;
using BookStore.Books;
using BookStore.Markers;
using BookStore.Reservations;
using BookStore.Stocks;
using System.Collections;

namespace BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<Reservation, ReservationDto>();
        CreateMap<Stock, StockDto>();
        CreateMap<CreateStockDto, StockDto>();
        CreateMap<CreateReservationDto, ReservationDto>();
        CreateMap<CreateReservationDto, Reservation>();
        CreateMap<Marker, MarkerDto>();
        CreateMap<CreateUpdateMarkerDto, Marker>();
    }
}
