using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public override async Task<IEnumerable<Room>> GetAll()
        {
            return await _db.Rooms.Include(x=>x.Category)
                .ThenInclude(x=>x.CategoryDates)
                .ToListAsync();
        }
    }
}