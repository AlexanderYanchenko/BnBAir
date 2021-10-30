using System;

namespace BnBAir.DAL.Enitities
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }


        public Reservation()
        {
            ReservationId = Guid.NewGuid();
        }
    }
}