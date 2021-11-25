using System;
using System.Collections.Generic;

namespace BnBAir.WEB.Models
{
    public class GuestModel
    {
        public Guid GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }

        public ICollection<ReservationModel> Reservations { get; set; }
    }
}