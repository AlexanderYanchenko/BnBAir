using System;
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
            return await _db.Rooms
                .Include(x=>x.Category)
                .ThenInclude(x=>x.CategoryDates)
                .ToListAsync();
        }
         public override async void Create(Room room, Guid? itemId)
         {
            var category = _db.Categories.FirstOrDefaultAsync(x => x.CategoryId == itemId);
            room.Category = await category;
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
         }
    }
}