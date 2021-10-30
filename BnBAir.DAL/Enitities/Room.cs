using System;

namespace BnBAir.DAL.Enitities
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public int Number { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Room()
        {
            RoomId = Guid.NewGuid();
        }
    }
}