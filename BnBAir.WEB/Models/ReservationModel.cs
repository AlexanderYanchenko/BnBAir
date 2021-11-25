using System;

namespace BnBAir.WEB.Models
{
    public class ReservationModel
    {
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        
        public virtual GuestModel Guest { get; set; }
        public virtual RoomModel Room { get; set; }
    }
}