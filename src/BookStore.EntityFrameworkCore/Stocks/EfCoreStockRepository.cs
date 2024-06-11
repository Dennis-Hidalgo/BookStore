using BookStore.Authors;
using BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BookStore.Stocks
{
    public class EfCoreStockRepository
        : EfCoreRepository<BookStoreDbContext, Stock, Guid>,
        IStockRepository
    {
        public EfCoreStockRepository(
        IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }

        public async Task<List<Stock>> GetListAsync(
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
