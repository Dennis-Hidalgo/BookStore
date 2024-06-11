using BookStore.EntityFrameworkCore;
using BookStore.Stocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BookStore.Reservations
{
    public class EfCoreReservationRepository
        : EfCoreRepository<BookStoreDbContext, Reservation>,
        IReservationRepository
    {
        public EfCoreReservationRepository(
        IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }

        public async Task<List<Reservation>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .ToListAsync();
        }
    }
}
