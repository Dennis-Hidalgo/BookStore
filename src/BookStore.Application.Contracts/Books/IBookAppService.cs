﻿using BookStore.Books;
using BookStore.Reservations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Books;

public interface IBookAppService :
    ICrudAppService< //Defines CRUD methods
        BookDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto> //Used to create/update a book
{
    // ADD the NEW METHOD
    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    Task<ReservationDto> OnCreateReservationAsync(CreateReservationDto dto);
}
