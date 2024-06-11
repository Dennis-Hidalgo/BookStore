using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BookStore.Markers
{
    public class GetMarkerListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
