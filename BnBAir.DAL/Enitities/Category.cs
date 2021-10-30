using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BnBAir.DAL.Enitities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int Bed { get; set; }

        public Guid RoomId { get; set; }
        public ICollection<Room> Rooms { get; set; }

        public Guid CategoryDateId { get; set; }
        public CategoryDate CategoryDates { get; set; }
        
        
        public Category()
        {
            CategoryId = Guid.NewGuid();
        }

    }
}