using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace BookStore.Markers
{
    public class MarkerDto : AuditedEntityDto<Guid>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public MarkerType MarkerType { get; set; }
    }
}
