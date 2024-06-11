using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Markers
{
    public class CreateUpdateMarkerDto
    {
        [Required]
        public double Latitude { get; set; } = 0.0;
        [Required]
        public double Longitude { get; set; } = 0.0;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public MarkerType MarkerType { get; set; } = MarkerType.undefined;
    }
}
