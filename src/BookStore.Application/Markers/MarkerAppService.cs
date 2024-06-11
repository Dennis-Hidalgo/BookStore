using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Markers
{
    public class MarkerAppService :
    CrudAppService<
        Marker, //The Marker entity
        MarkerDto, //Used to show markers
        Guid, //Primary key of the marker entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMarkerDto>, //Used to create/update a marker
    IMarkerAppService //implement the IMarkerAppService
    {
        private readonly IRepository<Marker, Guid> _repository;
        public MarkerAppService(IRepository<Marker, Guid> repository)
        : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<MarkerDto>> GetMarkersAsync()
        {
            var markers = await _repository.ToListAsync();
            return ObjectMapper.Map<List<Marker>, List<MarkerDto>>(markers);
        }
    }
}
