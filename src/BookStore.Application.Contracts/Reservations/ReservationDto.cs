using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace BookStore.Reservations
{
    public class ReservationDto : EntityDto<Guid>
    {
        public Guid BookId {  get; set; }

        public IdentityUserDto UserDto { get; set; }

        public string BookName { get; set; }
    }


}
