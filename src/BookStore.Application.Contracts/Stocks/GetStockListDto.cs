using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Stocks
{
    public class GetStockListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
