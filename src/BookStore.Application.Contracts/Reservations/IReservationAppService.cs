using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Reservations
{
    public interface IReservationAppService : IApplicationService
    {

        Task DeleteAsync(Guid bookId, Guid userId);

        Task<PagedResultDto<ReservationDto>> GetListAsync(GetReservationListDto input);

    }
}
