using BookStore.Authors;
using BookStore.Books;
using BookStore.Permissions;
using BookStore.Stocks;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace BookStore.Reservations
{
    public class ReservationAppService : BookStoreAppService, IReservationAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly CurrentUser _currentUser;

        public ReservationAppService(IRepository<Book, Guid> bookRepository, IIdentityUserRepository identityUserRepository, IReservationRepository reservationRepository, CurrentUser currentUser)
        {
            _bookRepository = bookRepository;
            _reservationRepository = reservationRepository;
            _currentUser = currentUser;
            _identityUserRepository = identityUserRepository;
        }

        [Authorize(BookStorePermissions.Reservations.Create)]


        public async Task DeleteAsync(Guid bookId, Guid userId)
        {
            //var reservation = await _reservationRepository.
            //await _reservationRepository.DeleteAsync(reservation);
        }

        public async Task<PagedResultDto<ReservationDto>> GetListAsync(GetReservationListDto input)
        {
            //var queryable = await _reservationRepository.GetQueryableAsync();
            //var query = from reservation in queryable
            //            join book in await _bookRepository.GetQueryableAsync() on reservation.BookId equals book.Id
            //            join user in await _identityUserRepository. on reservation.UserId equals user.Id
            //            select new { reservation, book, user };
            return null;
        }
    }
}
