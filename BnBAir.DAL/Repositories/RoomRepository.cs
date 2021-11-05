using System.Collections.Generic;
using System.Linq;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        private ReservationContext _db;
        public RoomRepository(ReservationContext db)
            : base(db, db.Rooms)
        {
            _db = db;
        }
        
        public virtual IEnumerable<Room> GetAll()
        {
            return _db.Rooms.Include(x=>x.Category.CategoryId).ToList();
        }
    }
}