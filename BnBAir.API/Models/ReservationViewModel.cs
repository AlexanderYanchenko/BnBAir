using System;
using BnBAir.DAL.Enitities;

namespace BnBAir.API.Models
{
    public class ReservationViewModel
    {
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        
        public virtual GuestViewModel Guest { get; set; }
        public virtual RoomViewModel Room { get; set; }
    }
}