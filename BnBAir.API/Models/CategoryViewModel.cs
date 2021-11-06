using System;
using System.Collections.Generic;

namespace BnBAir.API.Models
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int Bed { get; set; }

        public virtual ICollection<RoomViewModel> Rooms { get; set; }

        public virtual ICollection<CategoryDatesViewModel> CategoryDates { get; set; }
    }
}