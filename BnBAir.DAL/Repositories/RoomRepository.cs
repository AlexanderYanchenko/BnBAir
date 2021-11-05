using System.Collections.Generic;
using System.Linq;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        public readonly ReservationContext _db;
        public RoomRepository(ReservationContext db)
            : base(db, db.Rooms)
        {
            _db = db;
        }
        
        public override IEnumerable<Room> GetAll()
        {
            return _db.Rooms.Include(x=>x.Category).ToList();
        }
    }
}