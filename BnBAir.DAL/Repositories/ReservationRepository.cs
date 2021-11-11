using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>
    {
        private readonly ReservationContext _db;
        public ReservationRepository(ReservationContext db)
            : base(db, db.Reservations)
        {
            _db = db;
        }
        
        public override async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _db.Reservations
                        .Include(reservation => reservation.Guest)
                        .Include(reservation => reservation.Room)
                        .ThenInclude(room=>room.Category)
                        .ThenInclude(category=>category.CategoryDates)
                        .ToListAsync();
        }
        public override async void Create(Reservation reservation, Guid? itemId)
        {
            var room = _db.Rooms.FirstOrDefault(x => x.RoomId == itemId);
            reservation.Room = room;
            await _db.Reservations.AddAsync(reservation);
            await _db.SaveChangesAsync();
        }
    }
}