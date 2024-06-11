using BookStore.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Stocks
{
    public interface IStockRepository : IRepository<Stock, Guid>
    {
        //Task<Stock> FindByNameAsync(string name);

        Task<List<Stock>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
