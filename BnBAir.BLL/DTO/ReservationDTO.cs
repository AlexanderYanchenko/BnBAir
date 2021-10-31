using System;
using BnBAir.DAL.Enitities;

namespace BnBAir.BLL.DTO
{
    public class ReservationDTO
    {
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        
        public Guid GuestId { get; set; }
        public GuestDTO Guest { get; set; }

        public Guid RoomId { get; set; }
        public RoomDTO Room { get; set; }

    }
}