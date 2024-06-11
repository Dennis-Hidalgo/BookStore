using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Stocks
{
    public interface IStockAppService : IApplicationService
    {
        Task<StockDto> GetAsync(Guid id);

        Task<PagedResultDto<StockDto>> GetListAsync(GetStockListDto input);

        Task<StockDto> CreateAsync(CreateStockDto input);

        Task UpdateAsync(Guid id, CreateStockDto input);

        Task DeleteAsync(Guid id);

    }
}
