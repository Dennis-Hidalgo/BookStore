using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Stocks
{
    public class CreateStockDto : EntityDto<Guid>
    {
        [Required]
        public Guid BookId { get; set; }

        public string BookName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
