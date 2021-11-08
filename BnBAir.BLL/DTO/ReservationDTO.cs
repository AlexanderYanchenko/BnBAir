using System;
using BnBAir.BLL.Interfaces;
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
        
        public virtual GuestDTO Guest { get; set; }
        
        public virtual RoomDTO Room { get; set; }

    }
}