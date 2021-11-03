
using System;
using System.Collections.Generic;
using BnBAir.DAL.Enitities;

namespace BnBAir.BLL.DTO
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int Bed { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<CategoryDate> CategoryDates { get; set; }
        
    }
}