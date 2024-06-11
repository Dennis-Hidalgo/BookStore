using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using BookStore.Stocks;
using BookStore.Books;
using BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Services;
using BookStore.Authors;
using Volo.Abp.Domain.Entities;

namespace BookStore.Stocks
{
    [Authorize(BookStorePermissions.Stocks.Default)]
    public class StockAppService : BookStoreAppService, IStockAppService
    {

        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IStockRepository _stockRepository;
        public StockAppService(IRepository<Book, Guid> repository, IStockRepository stockRepository)
        {
            _bookRepository = repository;
            _stockRepository = stockRepository;
        }


        public async Task<StockDto> CreateAsync(CreateStockDto input)
        {
            var stock = await _stockRepository.InsertAsync(
                new Stock(
                    input.Quantity,
                    input.BookId
                    )
                ) ;
            var stockDto = ObjectMapper.Map<Stock, StockDto>( stock ) ;
            return stockDto;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<StockDto> GetAsync(Guid id)
        {
            var queryable = await _stockRepository.GetQueryableAsync();
            var query = from stock in queryable
                        join book in await _bookRepository.GetQueryableAsync() on stock.BookId equals book.Id
                        where stock.Id == id
                        select new { stock, book };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Stock), id);
            }

            var stockDto = ObjectMapper.Map<Stock, StockDto>(queryResult.stock);
            stockDto.BookId = queryResult.book.Id;
            return stockDto;
        }

        public async Task<PagedResultDto<StockDto>> GetListAsync(GetStockListDto input)
        {
            var queryable = await _stockRepository.GetQueryableAsync();
            var query = from stock in queryable
                        join book in await _bookRepository.GetQueryableAsync() on stock.BookId equals book.Id
                        select new { stock, book };
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var stockDtos = queryResult.Select(x =>
            {
                var stockDto = ObjectMapper.Map<Stock, StockDto>(x.stock);
                stockDto.BookName = x.book.Name;
                return stockDto;
            }).ToList();
            var totalCount = await _stockRepository.GetCountAsync();

            return new PagedResultDto<StockDto>(
                totalCount,
                stockDtos
            );
        }

        public async Task UpdateAsync(Guid id, CreateStockDto input)
        {
            var stock = await _stockRepository.GetAsync(id);
            stock.Quantity = input.Quantity;
            await _stockRepository.UpdateAsync(stock);
        }
    }
}
