using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Markers
{
    public class Marker : AuditedAggregateRoot<Guid>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public MarkerType MarkerType { get; set; }
    }
}
