using System;
using BnBAir.DAL.Enitities;

namespace BnBAir.BLL.DTO
{
    public class RoomDTO
    {
        public Guid RoomId { get; set; }
        public int Number { get; set; }
        public virtual CategoryDTO Category { get; set; }
    }
}