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

        public Guid RoomId { get; set; }
        public ICollection<RoomDTO> Rooms { get; set; }

        public Guid CategoryDateId { get; set; }
        public CategoryDateDTO CategoryDates { get; set; }
    }
}