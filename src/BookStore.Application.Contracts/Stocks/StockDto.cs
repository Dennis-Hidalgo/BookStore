using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Stocks
{
    public class StockDto : EntityDto<Guid>
    {
        public Guid BookId { get; set; }
        public string BookName {  get; set; }
        public int Quantity { get; set; }
    }
}
