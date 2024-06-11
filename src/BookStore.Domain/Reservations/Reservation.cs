using BookStore.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace BookStore.Reservations
{
    public class Reservation : Entity
    {

        public Guid BookId { get; set; }

        public Guid UserId { get; set; }

        public DateTime RegisterDate { get; set; }

        public Reservation() { }

        public Reservation(Guid? bookId, Guid userId, DateTime reservationDate )
        {

        }

        public virtual IdentityUser User { get; set; }

        public virtual Book Book { get; set; }
        public override object?[] GetKeys()
        {
            return [BookId, UserId];
        }
    }
}
