using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Reservations
{
    public class GetReservationListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
