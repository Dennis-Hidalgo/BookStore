using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Reservations
{
    public class CreateReservationDto : EntityDto
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
