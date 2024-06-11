using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Stocks
{
    public class Stock : FullAuditedAggregateRoot<Guid>
    {
        public int Quantity {  get; set; }
        public Guid BookId {  get; set; }


        public Stock() { }
        public Stock(
            int quantity,
            Guid bookId)
        {
            SetQuantity(quantity);

        }

        internal void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
        }
    }
}
