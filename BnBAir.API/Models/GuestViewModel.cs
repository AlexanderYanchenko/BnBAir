using System;
using System.Collections.Generic;

namespace BnBAir.API.Models
{
    public class GuestViewModel
    {
        public Guid GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }

        public ICollection<ReservationViewModel> Reservations { get; set; }
    }
}