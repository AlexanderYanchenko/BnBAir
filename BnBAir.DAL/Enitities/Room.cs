using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BnBAir.DAL.Enitities
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public int Number { get; set; }
        
        public virtual Category Category { get; set; }

        public Room()
        {
            RoomId = Guid.NewGuid();
        }
    }
}