using System;
using System.Collections.Generic;

namespace BnBAir.WEB.Models
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int Bed { get; set; }

        public virtual ICollection<RoomModel> Rooms { get; set; }

        public virtual ICollection<CategoryDateModel> CategoryDates { get; set; }
    }
}