using System;
using System.Collections.Generic;

namespace BnBAir.DAL.Enitities
{
    public class Guest
    {
        public Guid GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }
        
        public Guid ReservationId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public Guest()
        {
            GuestId = Guid.NewGuid();
        }
    }
}