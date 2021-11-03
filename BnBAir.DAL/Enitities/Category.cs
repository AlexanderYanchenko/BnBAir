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

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

        public virtual ICollection<CategoryDate> CategoryDates { get; set; } = new List<CategoryDate>();
        
        
        public Category()
        {
            CategoryId = Guid.NewGuid();
        }

    }
}