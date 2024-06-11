using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Markers
{
    public interface IMarkerAppService :
    ICrudAppService< //Defines CRUD methods
        MarkerDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMarkerDto> //Used to create/update a book
{
        public Task<List<MarkerDto>> GetMarkersAsync();
}
}
