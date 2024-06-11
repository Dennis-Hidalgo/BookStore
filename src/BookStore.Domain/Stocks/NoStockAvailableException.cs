using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace BookStore.Stocks
{
    public class NoStockAvailableException : BusinessException
    {
        public NoStockAvailableException(int quantity)
            : base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("quantity", quantity);
        }
    }
}
