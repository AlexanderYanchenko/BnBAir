using System;

namespace BnBAir.WEB.Models
{
    public class RoomModel
    {
        public Guid RoomId { get; set; }
        public int Number { get; set; }
        public virtual CategoryModel Category { get; set; }
    }
}