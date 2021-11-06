using System;

namespace BnBAir.API.Models
{
    public class RoomViewModel
    {
        public Guid RoomId { get; set; }
        public int Number { get; set; }
        public virtual CategoryViewModel Category { get; set; }
    }
}